using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Player player;
    public List<int> itemList;
    public Text tip;

    public enum ITEM_TYPE
    {
        REDPOTION = 1,
        BLUEPOTION = 2,
        PINKPOTION = 3,
        GREENPOTION = 4,
        WHITEPOTION = 5,
        end
    }
    
    public enum ITEM_INFO
    {
        HP = 1,
        attackDelay = 2,
        moveVelocity = 3,
        threeAttack = 4,
        end
    }

    public void GetItem(string name)
    {
        if (!tip.IsActive())
            tip.gameObject.SetActive(true);

        if (name.Equals("RedPotion"))
        {
            itemList.Add(1);
        }
        else if(name.Equals("BluePotion"))
        {
            itemList.Add(2);
        }
        else if(name.Equals("GreenPotion"))
        {
            itemList.Add(3);
        }
        else if(name.Equals("PurplePotion"))
        {
            itemList.Add(4);
        }
        tip.text = name + "을 얻었습니다.";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CheckItemNull())
            {
                UseItem();
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (tip.IsActive())
                tip.gameObject.SetActive(false);
            else
                tip.gameObject.SetActive(true);
        }

    }

    private bool CheckItemNull()
    {
        if(itemList.Count == 0)
        {
            if(!tip.IsActive())
                tip.gameObject.SetActive(true);
            tip.text = "아이템이 없습니다.";
            return false;
        }
        return true;       
    }

    private void UseItem()
    {
        if(itemList[0].Equals(1))
        {
            ApplyItem(1);
        }
        else if(itemList[0].Equals(2))
        {
            ApplyItem(2);
        }
        else if (itemList[0].Equals(3))
        {
            ApplyItem(3);
        }
        else if (itemList[0].Equals(4))
        {
            ApplyItem(4);
        }
        itemList.RemoveAt(0);
    }

    private void ApplyItem(int info)
    {
        if (info.Equals(1))
        {
            player.hp += 20;
            if (player.hp >= 100)
                player.hp = 100;
        }
        else if(info.Equals(2))
        {
            player.shootInterval -= 0.5;
            player.GetComponent<Animator>().SetFloat("attackSpeed", 1.2f);
        }
        else if(info.Equals(3))
        {
            player.speedByItem *= 1.2f;
        }
        else if(info.Equals(4))
        {
            player.GetComponentInChildren<Gun>().SetItemEffect();
        }
    }
}
