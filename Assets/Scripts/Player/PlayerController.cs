using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public float gravity = -35f;
    public float jumpHeight = 2f;
    public float walkSpeed = 3f;
    private float dashSpeed = 12.5f;
    private float dashTime = 0.35f;
    public float dashCooldownTime = 5f;
    private float attackAnimationTimer = 0.4f;
    private float _dashTimer = 0f;
    private float _attackTimer = 0f;
    private float _landingTimer = 0f;
    private float _landTime = 0.2f;
    private float _damageTimer = 0.2f;
    private float _damageTime = 0f;
    private BoxCollider2D _playerCollider;
    Vector3 _damageFallbackDirection;
    // State variables
    // NOTE: enum?
    private enum playerState
    {
        TAKINGDAMAGE,
        DASHING,
        ATTACKING,
        AIRATTACKING,
        LANDING,
        FREE
    }
    playerState _currentState;
    private bool _canDash = true;
    private bool _wasLanded = true;
    private float _prevY;

    private CharacterController2D _controller;
    private AnimationController2D _animator;
    public bool _isFacingRight = true;

    /// <summary>
    /// Timer control
    /// </summary>
    [SerializeField]
    private TimerController _timer;

    /// <summary>
    /// Manager of the game session
    /// </summary>
    [SerializeField]
    private GameManager _gameManager;

    /// <summary>
    /// Damage to player's timer
    /// </summary>
    [SerializeField]
    private float timerDamage;

    /// <summary>
    /// Controls the total amount of currency this player has access to. 
    /// </summary>
    public CurrencyController BankAccount;

    /// <summary>
    /// Contains all the pertinent information needed for each relevant object 
    /// controlled by this object. 
    /// </summary>
    private TheBrain brain;

    [SerializeField]
    private AttackColliderController _attackColliderController;

    private string _walkAnimation = "ShibaWalk";
    private string _jumpAnimation = "ShibaJump";
    private string _idleAnimation = "ShibaIdle";
    private string _dashAnimation = "ShibaDash";
    private string _attackAnimation = "ShibaAttackG";
    private string _landAnimation = "ShibaLand";
    [SerializeField]
    private string _victoryAnimation;
    private string _fallAnimation = "ShibaFall";
    private string _attackAirAnimation = "ShibaAttackA";
    private string _damageAnimation = "ShibaDam";

    public GameObject _Bomb;
    public int _bombsPlaced = 0;
    private float _bombCooldownTimer = 0f;
    private float _bombCooldownWaitTime = 0.5f;
    private bool _canPlaceBomb = true;

    private bool shopping = false;

    // Boolean to control if the player has infinite bomb placement
    private bool infiniteBombs = false;

    // Boolean to determine if we are debugging or not. 
    public bool debugMode = false;

    // Use this for initialization
    void Start () {
        _currentState = playerState.FREE;
        // TODO: Discover a more efficient way to find the below game object
        // I made it worse, but this was to ensure enstantiation of the brain on startup.
        //  I don't think there will be horrible performance issues. -JW
        if (GameObject.Find("TheBrain") != null)
        {
            brain = GameObject.Find("TheBrain").GetComponent<TheBrain>();
        }
        else
        {
            Debug.Log("The Brain was not found for this object");
            brain = new TheBrain();
            debugMode = true;
        }

        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);

        if (brain.currIncense != TheBrain.IncenseTypes.None)
        {
            _timer.Intialize(brain.Time, brain.IncenseSprites[(int)brain.currIncense]);
        }
        else
        {
            _timer.Intialize(brain.Time, brain.IncenseSprites[0]);
        }
       

        if (! Regex.IsMatch(   SceneManager.GetActiveScene().name, "Shop") )
        {
            _timer.StartTimer();
        }
        else
        {
            infiniteBombs = true;
            shopping = true;
        }

        BankAccount.AddToBank( brain.PlayersMoney );
        _prevY = gameObject.transform.position.y;

        _playerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            brain.SetNextAvailableIncense();
            _timer.ChangeTimerBarSprite(brain.IncenseSprites[(int)brain.currIncense]);
        }

        if (Input.GetKeyDown(KeyCode.B) && debugMode)
        {
            infiniteBombs = !infiniteBombs;
        }

        // Check to see if player has eliminated all key enemies.
        if (!shopping && _gameManager.GetNumOfKeyEnemiesAlive() <= 0)
        {
            _animator.setAnimation(_victoryAnimation);
            _gameManager.LevelFinished();
        }
        else
        {
            // The order of importance of player actions is as follows:
            //  1. Dashing (will interupt all other actions and override them)
            //  2. Attacking (can attack anytime other than when dashing)
            //  3. Jumping/Falling
            //  4. Walking
            //  Falling is checked first, then dashing, then attacking, then other
            Vector3 velocity = _controller.velocity;
            velocity.x = 0;
            // Keeps track of the amount of bombs the player has at this frame
            int BombTotal = brain.playerItemCounts[TheBrain.ItemTypes.Bomb];
            if (!_canPlaceBomb)
            {
                _bombCooldownTimer += Time.deltaTime;
                if (_bombCooldownTimer >= _bombCooldownWaitTime)
                {
                    _canPlaceBomb = true;
                }
            }
            //private float _damageTimer = 0.2f;
            //private float _damageTime = 0f;
            // If the player is currently taking damage...
            if (_currentState == playerState.TAKINGDAMAGE)
            {
                _animator.setAnimation(_damageAnimation);
                _damageTime += Time.deltaTime;
                _playerCollider.enabled = false;
                if (_damageTime >= _damageTimer)
                {
                    _currentState = playerState.FREE;
                    _damageTime = 0;
                    _playerCollider.enabled = true;
                    _animator.setAnimation("ShibaIdle");
                }
                else
                {
                    velocity.y = 0;
                    velocity -= _damageFallbackDirection * 500 * Time.deltaTime;
                    // Fall!
                    //velocity.y += gravity * Time.deltaTime;
                }
            }
            // If the player has left the ground...
            else if (!_controller.isGrounded)
            {
                _wasLanded = false;
                // ...or is falling.
                // Checking the y is important- if a player is on the rising 
                //  arc of their jump they shouldn't be falling. Also if the player
                //  is aerial attacking do not switch the animation
                if (_prevY > gameObject.transform.position.y && !(_currentState == playerState.AIRATTACKING))
                    _animator.setAnimation(_fallAnimation);
            }
            // ...and just landed.
            if (!_wasLanded && _controller.isGrounded && _currentState != playerState.TAKINGDAMAGE)
            {
                _wasLanded = true;
                _currentState = playerState.LANDING;
            }
            // If the player can't dash, incrememnt their cooldown timer.
            if (!_canDash)
            {
                // If they've done their time, allow the ability again
                if (dashCooldownTime + dashTime < _dashTimer)
                {
                    _canDash = true;
                    _dashTimer = 0;
                }
                else
                    _dashTimer += Time.deltaTime;
            }
            // Dashing overrides all other movements (except for the player taking damage)
            if (((Input.GetKeyDown("k") && _canDash) || (_currentState == playerState.DASHING)) && _currentState != playerState.TAKINGDAMAGE)
            {
                // Player doesn't fall
                velocity.y = 0;
                _animator.setAnimation(_dashAnimation);
                // If the player has just begun dashing,
                //  allow them to attack.
                if (_currentState != playerState.DASHING)
                {
                    _currentState = playerState.DASHING;
                    _attackColliderController.setEnabled(true);
                }
                // Move in the direction they are facing
                if (_isFacingRight)
                    velocity.x = dashSpeed;
                else
                    velocity.x = dashSpeed * -1;
                // Increment the timer for how long they can dash for.
                _dashTimer += Time.deltaTime;
                // Check to see if they are finished dashing.
                if (_dashTimer > dashTime)
                {
                    _currentState = playerState.FREE;
                    _canDash = false;
                    _animator.setAnimation(_idleAnimation);
                }
            }

            // Only perform other checks if not dashing or attacking
            else if (_currentState != playerState.TAKINGDAMAGE)
            {
                if (_currentState == playerState.ATTACKING || _currentState == playerState.AIRATTACKING)
                {
                    // attackAnimationTimer is how long they are in an attack state
                    _attackTimer += Time.deltaTime;
                    // Check to see if they are finished
                    if (_attackTimer > attackAnimationTimer)
                    {
                        _currentState = playerState.FREE;
                        _attackTimer = 0;
                    }
                    if (_currentState == playerState.ATTACKING) return;
                }
                // If walking left
                if (Input.GetAxis("Horizontal") < 0)
                {
                    velocity.x = walkSpeed * -1;
                    _animator.setFacing("Left");
                    if (_controller.isGrounded) _animator.setAnimation(_walkAnimation);
                    _isFacingRight = false;
                }
                // If walking right
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    velocity.x = walkSpeed;
                    _animator.setFacing("Right");
                    if (_controller.isGrounded) _animator.setAnimation(_walkAnimation);
                    _isFacingRight = true;
                }
                else
                {
                    // If the player is on the ground, they either just landed
                    //  or are in an idle state
                    if (_controller.isGrounded)
                    {
                        // Idling
                        if (_currentState != playerState.LANDING)
                            _animator.setAnimation(_idleAnimation);
                        // Landing
                        else
                        {
                            _animator.setAnimation(_landAnimation);
                            _landingTimer += Time.deltaTime;
                            if (_landingTimer > _landTime)
                            {
                                _landingTimer = 0;
                                _currentState = playerState.FREE;
                            }
                        }
                    }
                }
                // If the player tries to jump, only allow it if they are grounded.
                if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
                {
                    velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
                    _animator.setAnimation(_jumpAnimation);
                }
                // The player has initiated an attack...
                if (Input.GetKeyDown("j"))
                {
                    // ...perform a ground attack.
                    if (_controller.isGrounded)
                    {
                        _animator.setAnimation(_attackAnimation);
                        _currentState = playerState.ATTACKING;
                    }
                    // ...perform an aerial attack
                    else
                    {
                        _animator.setAnimation(_attackAirAnimation);
                        _currentState = playerState.AIRATTACKING;
                    }
                    _attackColliderController.setEnabled(true);
                    
                }
                if (Input.GetKeyDown("l") && _bombsPlaced < 2 && (BombTotal > 0 || infiniteBombs))
                {
                    if (_canPlaceBomb)
                    {
                        _canPlaceBomb = false;
                        GameObject bomb = Instantiate(_Bomb, transform.position, Quaternion.identity) as GameObject;
                        _bombsPlaced++;
                        bomb.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(transform.forward);//.AddForce(transform.forward * _bombThrust);
                        if (!infiniteBombs)
                        {
                            brain.playerItemCounts[TheBrain.ItemTypes.Bomb] = BombTotal > 1 ? BombTotal - 1 : 0;
                        }

                        //Debug.Log("Removed Bomb Ability : " + brain.playerItemCounts[TheBrain.ItemTypes.Bomb]);
                    }

                }
                // Fall!
                velocity.y += gravity * Time.deltaTime;
            }
            // Save the y, so on the next frame we can check if the player is falling.
            _prevY = gameObject.transform.position.y;
            // Perform the move.
            _controller.move(velocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Player Dies
    /// </summary>
    public void Die()
    {
        _gameManager.GameOver();
    }

    /// <summary>
    /// If player has collided with enemy attack, this event will fire off. 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (_currentState != playerState.ATTACKING && _currentState != playerState.AIRATTACKING && _currentState != playerState.DASHING) {
                _timer.ReduceTimer(timerDamage);
                _currentState = playerState.TAKINGDAMAGE;
                BoxCollider2D otherCollider = other.gameObject.GetComponent<BoxCollider2D>();
                Vector3 otherPos = other.gameObject.transform.position;
                Vector3 myPos = gameObject.transform.position;
                _damageFallbackDirection = otherPos - myPos;
                _damageFallbackDirection.z = 0;
                _damageFallbackDirection.Normalize();
            }
        }
        else if (other.tag == "KillZ")
        {
            _gameManager.GameOver();
        }
        else if (other.tag == "Coin")
        {
            BankAccount.AddToBank(CurrencyController.CurrencyTypes.Coin);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Jewel")
        {
            BankAccount.AddToBank(CurrencyController.CurrencyTypes.Jewel);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Gate")
        {
            _gameManager.NextLevel();
        }

    }


    /// <summary>
    /// Will add the correct number of items to players ability inventory. 
    /// </summary>
    /// <param name="item"></param>
    private void AddToPlayerInventory(ItemManager item)
    {
        int BombTotal = brain.playerItemCounts[TheBrain.ItemTypes.Bomb];
        switch (item.ItemTag)
        {
            case (TheBrain.ItemTypes.Dash):
                Debug.Log("Added Dash Ability");
                break;
            case (TheBrain.ItemTypes.Bomb):
                Debug.Log("Added Bomb Ability : " + ++brain.playerItemCounts[TheBrain.ItemTypes.Bomb]);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Shows the current time
    /// </summary>
    public void ShowTimer()
    {
        _timer.TimerDisplay();
    }

    /// <summary>
    /// Converts the given time to currency for the player
    /// </summary>
    /// <param name="amount"></param>
    public void ConvertTimeToCurrency()
    {
        long amount = BankAccount.ValueOfCurrency(CurrencyController.CurrencyTypes.Time);
        if (_timer.CurrTime - amount > 0)
        {
            _timer.ReduceTimer(amount);
            BankAccount.AddToBank(CurrencyController.CurrencyTypes.Time);
        }
    }

    /// <summary>
    /// When this game object is destroyed, we need to record the players time and total money
    /// </summary>
    void OnDestroy ()
    {
        if (brain.SingletonCreated)
        {
            brain.Time = _timer.CurrTime / 60;
            brain.PlayersMoney = BankAccount.BankAccount;
            print("players money saved");
        }
        //else
        //{
        //    Destroy(brain.gameObject);
        //    print("brain destroyed");
        //}
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (_currentState != playerState.ATTACKING && _currentState != playerState.AIRATTACKING && _currentState != playerState.DASHING)
            {
                _timer.ReduceTimer(timerDamage);
                _currentState = playerState.TAKINGDAMAGE;
                BoxCollider2D otherCollider = other.gameObject.GetComponent<BoxCollider2D>();
                Vector3 otherPos = other.gameObject.transform.position;
                Vector3 myPos = gameObject.transform.position;
                _damageFallbackDirection = otherPos - myPos;
                _damageFallbackDirection.z = 0;
                _damageFallbackDirection.Normalize();
            }
        }
    }


    }
