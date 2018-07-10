using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;
using Yarn.Unity;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class CheckDialog : MonoBehaviour {
	
	public float minPosition = -5.3f;
	public float maxPosition = 5.3f;

	public float moveSpeed = 1.0f;

	public float interactionRadius = 2.0f;

	public float movementFromButtons {get;set;}

	//character control
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}

	/// Draw the range at which we'll start talking to people.
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;

		// Flatten the sphere into a disk, which looks nicer in 2D games
		Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1,1,0));

		// Need to draw at position zero because we set position in the line above
		Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(0);
		}

		// Remove all player control when we're in dialogue
		if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
			return;
		}

		if (!m_Jump && GetComponent<Rigidbody2D> ().velocity.y == 0)
		{
			//Debug.Log (GetComponent<Rigidbody2D> ().velocity.y == 0);
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}

		// Detect if we want to start a conversation
		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckForNearbyNPC ();
		}
	}

	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		//float h = CrossPlatformInputManager.GetAxis("Horizontal");

		// Move the player, clamping them to within the boundaries 
		// of the level.
		var movement = CrossPlatformInputManager.GetAxis("Horizontal");
		movement += movementFromButtons;
		//movement *= (moveSpeed * Time.deltaTime);

		//var newPosition = transform.position;
		//newPosition.x += movement;
		//newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);

		//transform.position = newPosition;

		// Pass all parameters to the character control script.
		m_Character.Move(movement, crouch, m_Jump);
		m_Jump = false;
	}

	/// Find all DialogueParticipants
	/** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
	public void CheckForNearbyNPC ()
	{
		var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
		var target = allParticipants.Find (delegate (NPC p) {
			return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
				(p.transform.position - this.transform.position)// is in range?
					.magnitude <= interactionRadius;
		});
		if (target != null) {
			// Kick off the dialogue at this node.
			FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
		}
	}
}
