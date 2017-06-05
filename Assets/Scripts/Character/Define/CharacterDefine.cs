﻿using UnityEngine;
using System.Collections;

public enum Resp : byte
{
    TYPE_YES                  = 0,  //成功
    TYPE_NO                   = 1,  //不做
    TYPE_CANNOT_CONTROLSELF   = 2,  //角色被控制，无法操控

    TYPE_CANNOT_MOVE          = 3,  //角色无法移动 
    TYPE_CANNOT_MOVETODEST    = 4,  //无法到达此位置 
    TYPE_HAS_DEAD             = 5,  //角色已死亡

    TYPE_SKILL_LACKHP         = 11,  //缺少HP
    TYPE_SKILL_LACKMP         = 12,  //缺少MP
    TYPE_SKILL_LACKSP         = 13,  //缺少SP
    TYPE_SKILL_CASTING        = 14,  //正在释放技能
    TYPE_SKILL_LEADING        = 15,  //正在引导技能
    TYPE_SKILL_CD             = 16,  //技能还未冷却
    TYPE_SKILL_NOTFIND        = 17,  //找不到这个技能
    TYPE_SKILL_NOTDOATSCENE   = 18,  //当前场景无法使用技能
    TYPE_SKILL_LACKXP         = 19,  //缺乏经验

    TYPE_RIDE_ING             = 31,  //骑乘中
    TYPE_RIDE_NOTDOATSCENE    = 32,  //当前场景无法使用坐骑
    TYPE_RIDE_NOTDOATFSM      = 33,  //当前状态无法使用坐骑 
    TYPE_RIDE_NONE            = 34,  //当前你还没有坐骑  
}

public enum EMoveType : byte
{
    SeekPosition,
    SeekTransform,
    SeekActor,
    MoveForce,
}

public enum EAIState
{
    AI_NONE,  //无
    AI_IDLE,  //闲逛
    AI_FIGHT, //战斗
    AI_FOLLOW,//跟随
    AI_PATROL,//巡逻
    AI_DEAD,  //死亡
    AI_BACK,  //回家
    AI_CHASE, //追击
    AI_FLEE,  //避开
    AI_ESCAPE,//逃跑
    AI_BORN,  //出生
    AI_PLOT,  //剧情
    AI_GLOBAL,//全局
}

public enum EAITarget
{
    TYPE_SELF   = 0,
    TYPE_TARGET = 1,
    TYPE_HOST   = 2,
}

public enum FSMState : int
{
    FSM_EMPTY,
    FSM_BORN,                //出生
    FSM_IDLE,                //待机

    FSM_RUN,                 //快跑
    FSM_WALK,                //漫步

    FSM_SKILL,               //攻击
    FSM_DEAD,                //死亡
    FSM_REBORN,              //重生

    FSM_WOUND,               //受击
    FSM_BEATBACK,            //击退
    FSM_BEATDOWN,            //击倒
    FSM_BEATFLY,             //击飞
    FSM_FLOATING,            //浮空

    FSM_FROST,               //冰冻
    FSM_STUN,                //昏迷
    FSM_FIXBODY,             //定身
    FSM_VARIATION,           //变形
    FSM_FEAR,                //恐惧
    FSM_SLEEP,               //睡眠
    FSM_PARALY,              //麻痹
    FSM_BLIND,               //致盲

    FSM_RIDEMOVE,            //骑乘跑
    FSM_RIDEIDLE,            //骑乘待机
    FSM_RIDEATTACK,          //骑乘攻击

    FSM_TALK,                //说话
    FSM_HOOK,                //钩子
    FSM_GRAB,                //抓取
    FSM_FLY,                 //飞行
    FSM_ROLL,                //翻滚
    FSM_JUMP,                //跳跃

    FSM_DANCE,               //跳舞
    FSM_MINE,                //采集状态
    FSM_INTERACTIVE,         //交互
}

public enum EActorUnit
{
    Ground,//地面
    Heaven //天上
}

public enum EActorNature
{
    CAN_MOVE,          //可移动
    CAN_KILL,          //可击杀
    CAN_MANUALATTACK,  //可主动攻击
    CAN_TURN,          //可转向
    CAN_STUN,          //可击晕
    CAN_BEATBACK,      //可击退
    CAN_BEATFLY,       //可击飞
    CAN_BEATDOWN,      //可击倒
    CAN_WOUND,         //可受击
    CAN_REDUCESPEED,   //可减速
    CAN_FIXBODY,       //可定身
    CAN_SLEEP,         //可睡眠
    CAN_VARISTION,     //可变形
    CAN_PARALY,        //可麻痹
    CAN_FEAR,          //可恐惧
}

public enum EActorType
{
    PLAYER,   //玩家
    NPC,      //NPC
    MONSTER,  //怪物
    PET,      //宠物
    MOUNT,    //坐骑
    MACHINE,  //机关
    PARTNER,  //伙伴
}

public enum EActorSex
{
    B,//男
    G,//女
    X,//未知
}

//怪物类型
public enum EActorSort
{
    None   = 0,
    Normal = 1,   //正常
    Elite  = 2,   //精英
    Rare   = 3,   //稀有
    Boss   = 4,   //Boss
    World  = 5,   //世界Boss
    Chest  = 6,   //宝箱
    Tower  = 7,   //水晶塔
    Cage   = 8,   //囚笼
}

//怪物种族
public enum EActorRace
{
    TYPE_NONE    = 1,    //宝箱、囚笼等
    TYPE_HUMAN   = 2,    //人类
    TYPE_SPIRIT  = 3,    //精灵
    TYPE_ORC     = 4,    //兽人
    TYPE_GHOST   = 5,    //亡灵
    TYPE_DEVIL   = 6,    //恶魔
    TYPE_ELEM    = 7,    //元素
    TYPE_GIANT   = 8,    //巨人
    TYPE_MACHINE = 9,    //机械
    TYPE_BEAST   = 10,   //野兽
    TYPE_DRAGON  = 11,   //龙类
}


public enum EAttr
{
    HP         = 1,   //生命值
    AP         = 2,   //攻击力
    DF         = 3,   //防御力
    CRIT       = 4,   //爆击
    CRITDAMAGE = 5,   //爆伤
    MP         = 6,   //魔法值
    SUCK       = 7,   //吸血
    HIT        = 8,   //命中
    DODGE      = 9,   //闪避
    SP         = 10,  //灵力值

    INJURY     = 11,  //免伤
    HPRECOVER  = 12,  //回血
    MPRECOVER  = 13,  //回魔
    SPRECOVER  = 14,  //回灵力
    CRITDF     = 15,  //爆防
    SPEED      = 16,  //速度

    PAP        = 17,  //攻击百分比
    PHP        = 18,  //生命百分比
    PDF        = 19,  //防御百分比
    PMP        = 20,  //魔法百分比

    EAP        = 21,  //额外伤害
    EHP        = 22,  //攻击回血
    EMP        = 23,  //攻击回魔
    ESP        = 24,  //攻击回灵力

    IAP        = 25,  //冰攻击
    FAP        = 26,  //火攻击
    BAP        = 27,  //暗攻击
    LAP        = 28,  //电攻击

    IDF        = 29,  //冰攻击
    FDF        = 30,  //火攻击
    BDF        = 31,  //暗攻击
    LDF        = 32,  //电攻击

    MAXHP      = 33,  //最大生命值 
    MAXMP      = 34,  //最大魔法值
    MAXSP      = 35,  //最大灵力值
    ABSORB     = 36,  //伤害吸收
    REFLEX     = 37,  //伤害反弹

}

public enum ETargetCamp
{
    TYPE_NONE    =  0,
    TYPE_ALLY    =  1,
    TYPE_ENEMY   =  2,
    TYPE_NEUTRAL =  3,
}

public enum EPartnerSort
{
    LF = 1,
    RT = 2,
}

public enum EDeadReason
{
    Normal,   //正常死亡
    Dot,      //Dot
    Kill,     //机制秒杀
    Plot      //剧情杀
}

public enum ESkillPos
{
    Skill_0,
    Skill_1,
    Skill_2,
    Skill_3,
    Skill_4,
    Skill_5,
    Skill_6,
    Skill_7,
    Skill_8,
    Skill_9,
}

public enum ESyncDataType
{
    TYPE_ALL,
    TYPE_AVATAR,
    TYPE_BASEATTR,
    TYPE_CURRATTR,
    TYPE_BOARD,
    TYPE_AOI,
}