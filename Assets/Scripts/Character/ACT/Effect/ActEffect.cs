﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace ACT
{
    public class ActEffect : ActObj
    {
        [SerializeField]
        public EBind CasterBind = EBind.Head;

        public ActEffect()
        {
            this.EventType = EActEventType.Subtain;
        }

        protected override bool Trigger()
        {
            base.Trigger();
            if (World)
            {
                Transform p = Skill.Caster.Avatar.GetBindTransform(CasterBind);
                Unit = GTWorld.Instance.Ect.LoadEffect(ID, 0, Retain);
                Unit.CacheTransform.parent = p;
                Unit.CacheTransform.localPosition = Offset;
                Unit.CacheTransform.localEulerAngles = Euler;
                Unit.CacheTransform.parent = null;    
                Unit.CacheTransform.localScale = Scale;
                Unit.CacheTransform.localEulerAngles = Euler + Skill.Caster.Euler;
            }
            else
            {
                Transform p = Skill.Caster.Avatar.GetBindTransform(CasterBind);
                Unit = GTWorld.Instance.Ect.LoadEffect(ID, 0, KTransform.Create(Offset, Euler, Scale), p, Retain);
            }
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].ApplyCenter = Unit.CacheTransform;
                Children[i].ApplyHitPoint = Unit.Pos;
                Children[i].ApplyHitDir = Vector3.up;
            }
            Unit.Release(Duration);
            return true;
        }


        public override void Loop()
        {
            if (Status == EActStatus.INITIAL)
            {
                Begin();
            }
            if (PastTime < StTime)
            {
                return;
            }
            if (Status == EActStatus.SELFEND)
            {
                Exit();
                return;
            }
            if (Status == EActStatus.STARTUP)
            {
                Trigger();
            }
            Execute();
            ExecuteChildren();
            if (PastTime >= EdTime && Status == EActStatus.RUNNING)
            {
                End();
            }
        }
    }
}
