﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Xml;

public class DSkillTalent : DObj<int>
{
    public static readonly int[] TALENT_LEVELS  = { 10, 20, 30, 40, 50, 60 };
    public const int TYPE_NONE                  = 0;
    public const int TYPE_STRENG_SKILL          = 1;
    public const int TYPE_NEW_SKILL             = 2;
    public const int TYPE_NEW_AND_REPLACE_SKILL = 3;

    public int              Id;
    public string           Name = string.Empty;
    public int              Pos;
    public string           Icon = string.Empty;
    public int              Layer;
    public ECarrer          Carrer;
    public int              Type;
    public int              TargetSkillId;
    public string           Desc = string.Empty;

    public override int GetKey()
    {
        return Id;
    }

    public override void Read(XmlElement element)
    {
        this.Id            = element.GetInt("Id");
        this.Name          = element.GetString("Name");
        this.Pos           = element.GetInt("Pos");
        this.Icon          = element.GetString("Icon");
        this.Layer         = element.GetInt("Layer");
        this.Carrer        = (ECarrer)element.GetInt("Carrer");
        this.Type          = element.GetInt("TalentSkillType");
        this.TargetSkillId = element.GetInt("TargetSkillId");
        this.Desc          = element.GetString("Desc");
    }
}

public class ReadCfgSkillTalent : DReadBase<int, DSkillTalent>
{

}
