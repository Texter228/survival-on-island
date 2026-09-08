using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public Animator anim;

    public bool _isFacingRight = true;


    public bool isattack;

    public bool isanim;

    public ParticleSystem particleSystem;

    public FixedJoystick joystick;

    float x;
    float y;

    public float transformx, transformy;

    [SerializeField] private int eatplayer,maxeat;
    [SerializeField] private float minusspeed;

    [SerializeField] private hotbar Hotbar;

    void Start()
    {
        anim = GetComponent<Animator>();

        Load();
    }

    void FixedUpdate()
    {
        eatplayer = Hotbar.GetEat();
        maxeat = Hotbar.GetMaxEat();

        minusspeed = (maxeat - eatplayer) / 4;

        transformx = transform.position.x;
        transformy = transform.position.y;

        Save();

        x = joystick.Horizontal;
        y = joystick.Vertical;

        if(x == 0 && y == 0)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
        }

        transform.position += new Vector3(x, y, 0) * (speed - minusspeed)  * Time.deltaTime;

        if (x != 0 || y != 0)
        {
            anim.SetBool("run", true);
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            anim.SetBool("run", false);
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }

        Flip();
    }

    private void Flip()
    {
        if (_isFacingRight && x < 0f || !_isFacingRight && x > 0f)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void AnimStart(){
        isanim = true;
    }

    public void AnimAttack(){
        isattack = true;
    }

    public void AnimStop(){
        isanim = false;
    }

    private IEnumerator StopParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("x", transformx);
        PlayerPrefs.SetFloat("y", transformy);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            transformx = PlayerPrefs.GetFloat("x");
            transformy = PlayerPrefs.GetFloat("y");

            transform.position = new Vector2(transformx,transformy);
        }
    }
}
