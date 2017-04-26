using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private DieAndRespawn dieAndRespawn;
    private CharacterControl cc;
    private ParticleController par;
    private PlayerInventory inventory;

    // Use this for initialization
    void Start()
    {
        dieAndRespawn = gameObject.GetComponent<DieAndRespawn>();
        cc = gameObject.GetComponent<CharacterControl>();
        par = gameObject.GetComponent<ParticleController>();
        inventory = GameData.data.inventory;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WalkableWater"))
            cc.isWater(true);
        if (other.gameObject.CompareTag("SavePoint"))
        {
            GameData.data.posx = cc.gameObject.transform.position.x;
            GameData.data.posy = cc.gameObject.transform.position.y;
            GameData.data.posz = cc.gameObject.transform.position.z;
            SaveLoad.Save();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WalkableWater"))
            cc.isWater(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        else if (body.gameObject.tag == "Water")
        {
            dieAndRespawn.OnDied();
        }

        //Player pickup item here
        else if (body.gameObject.tag == "Item01")
        {
            if(!inventory.isInventoryFull() || inventory.pIList.FindIndex(a => a.names == new Item01().names) != -1)
            {
                inventory.addItem(new Item01());
                Destroy(body.gameObject);
                par.playParticle(new Color(1, 0, 0, 1));
            }
        }
        else if (body.gameObject.tag == "Equipment")
        {
            if (!inventory.isInventoryFull())
            {
                switch ((EquipmentType)Random.Range(1, 4))
                {
                    case (EquipmentType.Glove):
                        inventory.addItem(new Pohpae01());
                        break;

                    case (EquipmentType.Hat):
                        {
                            switch ((Hat)Random.Range(0, 3))
                            {
                                case (Hat.Helmet01):
                                    {
                                        Helmet01 hat = new Helmet01();
                                        inventory.addItem(hat);
                                        break;
                                    }

                                case (Hat.PaperHat):
                                    {
                                        PaperHat paper = new PaperHat();
                                        inventory.addItem(paper);
                                        break;
                                    }

                                case (Hat.StrawHat):
                                    {
                                        StrawHat straw = new StrawHat();
                                        inventory.addItem(straw);
                                        break;
                                    }
                            }
                            break;
                        }

                    case (EquipmentType.Suit):
                        inventory.addItem(new Robe01());
                        break;
                }
                Destroy(body.gameObject);
                par.playParticle(new Color(1, 1, 0, 1));
            }
        }
    }
}
