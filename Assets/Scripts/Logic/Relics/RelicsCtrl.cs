﻿using UnityEngine;
using System.Collections;
using System;
using ProtoBuf;
using Protocol;

public class RelicsCtrl : GTSingleton<RelicsCtrl> ,ICtrl
{
    public void DelEventListeners()
    {

    }

    public void AddEventListeners()
    {
        NetworkManager.AddListener(MessageID.MSG_ACK_BATTLE_RELICS, OnAck_BattleRelics);
        NetworkManager.AddListener(MessageID.MSG_ACK_UNLOAD_RELICS, OnAck_UnloadRelics);
        NetworkManager.AddListener(MessageID.MSG_ACK_CHARGE_RELICS, OnAck_ChargeRelics);
        NetworkManager.AddListener(MessageID.MSG_ACK_UPGRADE_RELICS, OnAck_UpgradeRelics);
    }

    private void OnAck_UpgradeRelics(MessageRecv obj, MessageRetCode retCode)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Packet.Data);
        AckUpgradeRelics ack = Serializer.Deserialize<AckUpgradeRelics>(ms);


        DRelics db = ReadCfgRelics.GetDataById(ack.RelicsID);
        XRelics relics = DataDBSRelics.GetDataById(ack.RelicsID);
        relics.Level++;
        relics.CurExp1 = relics.CurExp1 - db.LevelRequireExp[relics.Level - 1];
        relics.CurExp2 = relics.CurExp2 - db.LevelRequireExp[relics.Level - 1];
        relics.CurExp3 = relics.CurExp3 - db.LevelRequireExp[relics.Level - 1];
        DataDBSRelics.Update(ack.RelicsID, relics);

        GTEventCenter.FireEvent(GTEventID.TYPE_UPGRADE_RELICS, ack.RelicsID);
        GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_FIGHTVALUE);
    }

    private void OnAck_ChargeRelics(MessageRecv obj, MessageRetCode retCode)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Packet.Data);
        AckChargeRelics ack      = Serializer.Deserialize<AckChargeRelics>(ms);
        int             index    = ack.Index;
        int             relicsID = ack.RelicsID;

        DRelics db   = ReadCfgRelics.GetDataById(relicsID);
        int itemID     = db.ArtificeCostID[index - 1];
        DItem itemDB = ReadCfgItem.GetDataById(itemID);
        XRelics relics = DataDBSRelics.GetDataById(relicsID);
        if (relics == null)
        {
            relics = new XRelics();
            relics.Id = relicsID;
        }
        switch (index)
        {
            case 1:
                relics.CurExp1 += itemDB.Data1;
                break;
            case 2:
                relics.CurExp2 += itemDB.Data1;
                break;
            case 3:
                relics.CurExp3 += itemDB.Data1;
                break;
        }

        DataDBSRelics.Update(relicsID, relics);
        DataManager.Instance.UseItemById(itemID);

        GTEventCenter.FireEvent(GTEventID.TYPE_CHARGE_RELICS, relicsID, index);
        GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_FIGHTVALUE);
    }

    private void OnAck_UnloadRelics(MessageRecv obj, MessageRetCode retCode)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Packet.Data);
        AckUnloadRelics ack = Serializer.Deserialize<AckUnloadRelics>(ms);
       
        XCharacter role = RoleModule.Instance.GetCurPlayer();
        role.Relic = 0;
        DataDBSRole.Update(role.Id, role);

        GTEventCenter.FireEvent(GTEventID.TYPE_UNLOAD_RELICS, ack.RelicsID);
        GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_FIGHTVALUE);
    }

    private void OnAck_BattleRelics(MessageRecv obj, MessageRetCode retCode)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Packet.Data);
        AckBattleRelics ack = Serializer.Deserialize<AckBattleRelics>(ms);

        XCharacter role = RoleModule.Instance.GetCurPlayer();
        role.Relic = ack.RelicsID;
        DataDBSRole.Update(role.Id, role);

        GTEventCenter.FireEvent(GTEventID.TYPE_BATTLE_RELICS, ack.RelicsID);
        GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_FIGHTVALUE);
    }
}
