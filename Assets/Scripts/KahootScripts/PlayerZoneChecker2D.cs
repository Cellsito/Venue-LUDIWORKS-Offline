using UnityEngine;

public class PlayerZoneChecker2D : MonoBehaviour
{
    public int currentZone = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Resposta"))
        {
            currentZone = other.GetComponent<AnswerZone2D>().zoneIndex;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Resposta"))
        {
            currentZone = collision.GetComponent<AnswerZone2D>().zoneIndex;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentZone = -1;
    }

}
