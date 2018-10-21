using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class MagicFireProjectile : MonoBehaviour 
{
    RaycastHit hit;
    public GameObject projectiles = null;
    public Transform spawnPosition;
    [HideInInspector]
    //public int currentProjectile = 0;
	public float speed = 1000;

    public float attackSpeed = 0.5f;
    public bool canFire = true;

//    MyGUI _GUI;
	//MagicButtonScript selectedProjectileButton;

	void Start () 
	{
		//selectedProjectileButton = GameObject.Find("Button").GetComponent<MagicButtonScript>();
	}

	void Update () 
	{
            if (!gameObject.GetComponent<PhotonView>().isMine) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && projectiles != null)
        {

			if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
                {
                    canFire = false;

                        PhotonView ph = PhotonView.Get(this);
                       
                        ph.RPC("FireNetwork", PhotonTargets.All, spawnPosition.position.ToString(), hit.point.ToString(), hit.normal.ToString());
                        
                        attackSpeed = projectiles.GetComponent<Spell>().timer;
                        StartCoroutine(AttackSpeed());
                    }  
            }

        }
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction*100, Color.yellow);
	}
    public IEnumerator AttackSpeed()
     {
      yield return new WaitForSeconds(attackSpeed);
            canFire = true;
    }
   

   

	public void AdjustSpeed(float newSpeed)
	{
		speed = newSpeed;
            
	}

    [PunRPC]
    void FireNetwork(string pos, string hit, string nor)
    {
            Vector3 p = StringToVector3(pos);
            Vector3 r = StringToVector3(hit);
            Vector3 n = StringToVector3(nor);
        /*if (projectiles[currentProjectile].GetComponent<Spell>().type.Equals("Projectile"))
        {
            speed = projectiles[currentProjectile].GetComponent<Projectile>().speed;
        }*/
        
            GameObject projectile = Instantiate(projectiles, p, Quaternion.identity) as GameObject;
            projectile.transform.LookAt(r);
            speed = projectile.GetComponent<Projectile>().speed;
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
            projectile.GetComponent<MagicProjectileScript>().impactNormal = n;
           
        }
        public Vector3 StringToVector3(string sVector)
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            return result;
        }
    }
