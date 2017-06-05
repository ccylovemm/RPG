﻿using UnityEngine;
using System.Collections;
using System;
using Protocol;
using ProtoBuf;

public class RoleCtrl : GTSingleton<RoleCtrl>, ICtrl
{
    public void DelEventListeners()
    {

    }

    public void AddEventListeners()
    {
        NetworkManager.AddListener(MessageID.MSG_ACK_ADDHERO_EXP,         OnAck_AddHeroExp);
    } 

    private void OnAck_AddHeroExp(MessageRecv obj, MessageRetCode retCode)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Packet.Data);
        AckAddPlayerExp ack = Serializer.Deserialize<AckAddPlayerExp>(ms);

        XCharacter player = RoleModule.Instance.GetCurPlayer();
        int oldLevel = player.Level;

        int maxLevel = ReadCfgRoleLevel.Count;
        if (player.Level >= maxLevel)
        {
            return;
        }
        player.CurExp += ack.Exp;
        DRoleLevel levelDB = ReadCfgRoleLevel.GetDataById(player.Level);
        while (player.CurExp >= levelDB.RequireExp)
        {
            player.CurExp -= levelDB.RequireExp;
            player.Level++;
            if (player.Level >= maxLevel)
            {
                player.CurExp = 0;
                break;
            }
            levelDB = ReadCfgRoleLevel.GetDataById(player.Level);
        }
        DataDBSRole.Update(player.Id, player);

        GTItemHelper.ShowExpTip(ack.Exp);
        int newLevel = player.Level;
        if (newLevel > oldLevel)
        {
            GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_HEROLEVEL);
            GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_FIGHTVALUE);
        }
        GTEventCenter.FireEvent(GTEventID.TYPE_CHANGE_HEROEXP);
    }
}

