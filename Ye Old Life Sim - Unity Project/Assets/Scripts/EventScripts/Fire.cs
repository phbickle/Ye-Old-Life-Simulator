﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire : RandomEventManager
{
	public UseableItemInventory m_UseableInventory;
	private Food playerFood_;

	public string PlayEvent(PlayerData pData, string tData)
	{

		if (GetComponent<PlayerData>().m_Shillings >= 500)
		{
			m_MoneyLost = 500;
		}
		else
		{
			m_MoneyLost = GetComponent<PlayerData>().m_Shillings;
		}

		if (m_UseableInventory != null)
		{
			foreach (KeyValuePair<string, UseableItemInventory.ItemInventoryEntry> entry in m_UseableInventory.m_UseableItemInventory)
			{
				if (entry.Value.item is Food)
				{
					//set the food variable if the entry is a food type
					playerFood_ = (Food)entry.Value.item;
					playerFood_.RemoveFood();   //removes perishable food item
				}
			}
		}

		tData = "YOUR HOUSE CAUGHT FIRE. You lost all your food and  " + m_MoneyLost.ToString() + " shillings.";
		return tData;
		//pop up window
	}
	//
}
