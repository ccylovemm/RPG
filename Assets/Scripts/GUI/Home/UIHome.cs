﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Cfg.Task;
using Protocol;
using ACT;

public class UIHome : GTWindow
{
    class ItemFunc
    {
        public string name;
        public string icon;
        public UIEventListener.VoidDelegate onClick;
    }

    class ItemSkill
    {
        public GameObject btn;
        public UISprite   maskSprite;
        public UISprite   lockSprite;
        public UITexture  icon;
    }

    class ItemBuff
    {
        public GameObject item;
        public UISprite   mask;
        public UISprite   icon;
    }

    class ItemPartner
    {
        public GameObject go;
        public UISprite heroIcon;
        public UISprite heroQuality;
        public UILabel  heroLevel;
        public UILabel  heroName;
        public UISlider heroHPSlider;
        public UILabel  heroHPPercent;
    }

    enum FuncArea
    {
        Top,
        TopLeft,
        TopRight,
        Bottom,
        BottomLeft,
        BottomRight,
    }

    readonly List<ItemFunc> TopBtnTemps;
    readonly List<ItemFunc> BottomBtnTemps;
    readonly Dictionary<SceneType, FuncArea[]> ShowAreas;

    private List<GameObject> mBottomButtons = new List<GameObject>();
    private List<GameObject> mTopButtons = new List<GameObject>();
    private List<ItemBuff>   mBuffItems = new List<ItemBuff>();
    private Dictionary<ESkillPos, ItemSkill> mSkillBtns = new Dictionary<ESkillPos, ItemSkill>();
    private List<ItemPartner> mPartners = new List<ItemPartner>();

    private Transform mBottomRight;
    private Transform mBottomLeft;
    private Transform mTopLeft;
    private Transform mTopRight;
    private Transform mTopRightGrid;
    private Transform mTop;
    private Transform mBottom;

    private EJoystick  mNGUIJoystick;

    private UISlider mBar1;
    private UISlider mBar2;
    private UISlider mBar3;
    private UILabel  mBarValue1;
    private UILabel  mBarValue2;
    private UISprite mPlayerIcon;
    private UILabel  mPlayerLevel;
    private UILabel  mPlayerName;

    private GameObject btnMenu;
    private GameObject btnHero;
    private GameObject btnJump;
    private GameObject btnTemp;
    private GameObject btnRelics;
    private GameObject btnBag;

    private UIToggle menuTask;
    private UIToggle menuPartner;
    private Transform contentTask;
    private Transform contentPartner;
    private UILabel mTaskContentType1;
    private UILabel mTaskContentType2;

    public UIHome()
    {
        mResPath = "Home/UIHome";
        mResident = false;
        Type = EWindowType.WINDOW;

        TopBtnTemps = new List<ItemFunc>
        {

           new ItemFunc {name="副本",icon="427",onClick=OnRaidClick},
           new ItemFunc {name="商店",icon="425",onClick=OnShopClick},
           new ItemFunc {name="冒险",icon="428",onClick=OnAdventureClick},
           new ItemFunc {name="任务",icon="426",onClick=OnTaskClick},
           new ItemFunc {name="技能",icon="431",onClick=OnSkillClick},
           new ItemFunc {name="神器",icon="432",onClick=OnRelicsClick},
           new ItemFunc {name="坐骑",icon="422",onClick=OnMountClick},
           new ItemFunc {name="伙伴",icon="430",onClick=OnPartnerClick},
           new ItemFunc {name="宠物",icon="424",onClick=OnPetClick},
           new ItemFunc {name="设置",icon="432",onClick=OnSettingClick},
        };
    }


    protected override void OnAwake()
    {
        mTop = transform.Find("Top");
        mTopLeft = transform.Find("TopLeft");
        mTopRight = transform.Find("TopRight");
        mBottom = transform.Find("Bottom");
        mBottomRight = transform.Find("BottomRight");
        mBottomLeft = transform.Find("BottomLeft");
        mTopRightGrid = mTopRight.Find("Grid");

        btnMenu = mTopRight.Find("Pivot/Btn_Menu").gameObject;
        btnBag = mTopRight.Find("Pivot/Btn_Bag").gameObject;
        btnHero = mTopLeft.Find("Hero").gameObject;

        for (int i = 0; i < 7; i++)
        {
            ItemSkill tab = new ItemSkill();
            Transform btn = mBottomRight.Find(i.ToString());
            tab.btn = btn.gameObject;
            tab.lockSprite = btn.transform.Find("Lock").GetComponent<UISprite>();
            tab.maskSprite = btn.transform.Find("Mask").GetComponent<UISprite>();
            tab.icon = btn.transform.Find("Icon").GetComponent<UITexture>();
            tab.maskSprite.gameObject.SetActive(false);
            mSkillBtns.Add((ESkillPos)i, tab);
        }
        mNGUIJoystick = mBottomLeft.GetComponent<EJoystick>();

        mBar1 = mTopLeft.Find("Bar_1").GetComponent<UISlider>();
        mBar2 = mTopLeft.Find("Bar_2").GetComponent<UISlider>();
        mBar3 = mTopLeft.Find("Bar_3").GetComponent<UISlider>();
        mBarValue1 = mBar1.transform.Find("Num").GetComponent<UILabel>();
        mBarValue2 = mBar2.transform.Find("Num").GetComponent<UILabel>();
        mPlayerIcon = mTopLeft.Find("Hero/Icon").GetComponent<UISprite>();
        mPlayerLevel = mTopLeft.Find("Hero/Level").GetComponent<UILabel>();
        mPlayerName = mTopLeft.Find("Hero/Name").GetComponent<UILabel>();

        btnJump = mBottomRight.Find("Btn_Jump").gameObject;
        btnRelics = mBottomRight.Find("Btn_Relics").gameObject;
        btnTemp = transform.Find("BtnTemplate").gameObject;
        btnTemp.SetActive(false);

        for (int i = 1; i <= 4; i++)
        {
            ItemBuff tab = new ItemBuff();
            Transform trans = mTopLeft.transform.Find("Buff/" + i);
            tab.item = trans.gameObject;
            tab.item.SetActive(false);
            tab.icon = trans.Find("Icon").GetComponent<UISprite>();
            tab.mask = trans.Find("Mask").GetComponent<UISprite>();
            mBuffItems.Add(tab);
        }

        menuTask = mTopLeft.Find("Menus/Menu_Task").GetComponent<UIToggle>();
        menuPartner = mTopLeft.Find("Menus/Menu_Partner").GetComponent<UIToggle>();
        contentPartner = mTopLeft.Find("Menus/Content_Partner");
        contentTask = mTopLeft.Find("Menus/Content_Task");
        int group = GTWindowManager.Instance.GetToggleGroupId();
        menuTask.group = group;
        menuPartner.group = group;
        menuTask.value = true;
        mTaskContentType1 = contentTask.Find("Type1").GetComponent<UILabel>();
        mTaskContentType2 = contentTask.Find("Type2").GetComponent<UILabel>();

        for (int i = 1; i <= 2; i++)
        {
            Transform trans = contentPartner.Find(i.ToString());
            ItemPartner tab = new ItemPartner();
            tab.go = trans.gameObject;
            tab.heroIcon = trans.Find("Icon").GetComponent<UISprite>();
            tab.heroQuality = trans.Find("Quality").GetComponent<UISprite>();
            tab.heroName = trans.Find("Name").GetComponent<UILabel>();
            tab.heroLevel = trans.Find("Level").GetComponent<UILabel>();
            tab.heroHPSlider = trans.Find("HP").GetComponent<UISlider>();
            tab.heroHPPercent = trans.Find("HP/Num").GetComponent<UILabel>();
            tab.go.SetActive(false);
            mPartners.Add(tab);
        }
    }

    protected override void OnAddButtonListener()
    {
        UIEventListener.Get(btnMenu).onClick = OnMenuClick;
        UIEventListener.Get(btnHero).onClick = OnHeroClick;
        for (int i = 0; i < mSkillBtns.Count; i++)
        {
            GameObject btn = mSkillBtns[(ESkillPos)i].btn;
            UIEventListener.Get(btn).onClick = OnCastSkillClick;
        }
        UIEventListener.Get(btnJump).onClick = OnJumpClick;
        UIEventListener.Get(btnRelics).onClick = OnArtClick;
        UIEventListener.Get(btnBag).onClick = OnBagClick;
    }

    protected override void OnAddHandler()
    {
        mNGUIJoystick.On_JoystickMove += OnJoystickMove;
        mNGUIJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        mNGUIJoystick.On_JoystickTouchClick += OnJoystickTouchClick; 
        mNGUIJoystick.On_JoystickTouchDrag += OnJoystickMove;
        mNGUIJoystick.On_JoystickTouchDragEnd += OnJoystickMoveEnd;

        GTEventCenter.AddHandler(GTEventID.TYPE_UD_AVATAR_HP, OnUpdateAvatarHealth);
        GTEventCenter.AddHandler(GTEventID.TYPE_UD_AVATAR_MP, OnUpdateAvatarPower);
        GTEventCenter.AddHandler(GTEventID.TYPE_UD_AVATAR_ATTR, OnUpdateAttr);
        GTEventCenter.AddHandler(GTEventID.TYPE_UD_PARTNER_HP, OnRefreshPartner);
        GTEventCenter.AddHandler(GTEventID.TYPE_UD_AVATAR_BUFF, OnRefreshBuffItems);
        GTEventCenter.AddHandler(GTEventID.TYPE_CHANGE_HEROLEVEL, OnUpdateAvatarLevel);
        GTEventCenter.AddHandler(GTEventID.TYPE_CHANGE_HERONAME, OnUpdateAvatarName);
        GTEventCenter.AddHandler(GTEventID.TYPE_CHANGE_HEROHEAD, OnUpdateAvatarIcon);
        GTEventCenter.AddHandler<int, int>(GTEventID.TYPE_CHANGE_PARTNER, OnRefreshPartner);
        GTEventCenter.AddHandler(GTEventID.TYPE_ADVANCE_PARTNER, OnRefreshPartner);
        GTEventCenter.AddHandler(GTEventID.TYPE_UPGRADE_PARTNER, OnRefreshPartner);
        GTEventCenter.AddHandler(GTEventID.TYPE_THREAD_TASK_STATE, OnUpdateThreadTask);
        GTEventCenter.AddHandler(GTEventID.TYPE_CHANGE_HEROEXP, OnUpdateAvatarExp);

        GTUpdate.Instance.AddListener(OnUpdate);

    }

    protected override void OnDelHandler()
    {
        mNGUIJoystick.On_JoystickMove -= OnJoystickMove;
        mNGUIJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        mNGUIJoystick.On_JoystickTouchClick -= OnJoystickTouchClick;
        mNGUIJoystick.On_JoystickTouchDrag -= OnJoystickMove;
        mNGUIJoystick.On_JoystickTouchDragEnd -= OnJoystickMoveEnd;
        GTEventCenter.DelHandler(GTEventID.TYPE_UD_AVATAR_HP, OnUpdateAvatarHealth);
        GTEventCenter.DelHandler(GTEventID.TYPE_UD_AVATAR_MP, OnUpdateAvatarPower);
        GTEventCenter.DelHandler(GTEventID.TYPE_UD_PARTNER_HP, OnRefreshPartner);
        GTEventCenter.DelHandler(GTEventID.TYPE_UD_AVATAR_ATTR, OnUpdateAttr);
        GTEventCenter.DelHandler(GTEventID.TYPE_UD_AVATAR_BUFF, OnRefreshBuffItems);
        GTEventCenter.DelHandler(GTEventID.TYPE_CHANGE_HEROLEVEL, OnUpdateAvatarLevel);
        GTEventCenter.DelHandler(GTEventID.TYPE_CHANGE_HERONAME, OnUpdateAvatarName);
        GTEventCenter.DelHandler(GTEventID.TYPE_CHANGE_HEROHEAD, OnUpdateAvatarIcon);
        GTEventCenter.DelHandler<int, int>(GTEventID.TYPE_CHANGE_PARTNER, OnRefreshPartner);
        GTEventCenter.DelHandler(GTEventID.TYPE_ADVANCE_PARTNER, OnRefreshPartner);
        GTEventCenter.DelHandler(GTEventID.TYPE_UPGRADE_PARTNER, OnRefreshPartner);
        GTEventCenter.DelHandler(GTEventID.TYPE_THREAD_TASK_STATE, OnUpdateThreadTask);
        GTEventCenter.DelHandler(GTEventID.TYPE_CHANGE_HEROEXP, OnUpdateAvatarExp);

        GTUpdate.Instance.DelListener(OnUpdate);

    }

    protected override void OnEnable()
    {
        if (CharacterManager.Main == null)
        {
            return;
        }
        if (CharacterManager.Main.CacheTransform == null)
        {
            return;
        }
        InitButtons();
        ShowMenus();
        OnRefreshSkillItems();
        OnRefreshSkillIcons();
        OnRefreshBuffItems();
        OnRefreshPartner();
        OnUpdateAvatarHealth();
        OnUpdateAvatarPower();
        OnUpdateAvatarLevel();
        OnUpdateAvatarExp();
        OnUpdateAvatarName();
        OnUpdateThreadTask();
        OnUpdateBranchTask();
    }

    protected override void OnClose()
    {
        mBottomButtons.Clear();
        mTopButtons.Clear();
        mSkillBtns.Clear();
        mBuffItems.Clear();
        mPartners.Clear();
    }

    private void InitButtons()
    {
        UIGrid grid = mTopRightGrid.GetComponent<UIGrid>();
        for (int i = 0; i < TopBtnTemps.Count; i++)
        {
            ItemFunc data = TopBtnTemps[i];
            GameObject item = NGUITools.AddChild(mTopRightGrid.gameObject, btnTemp);
            grid.AddChild(item.transform);
            item.SetActive(true);
            item.transform.Find("Name").GetComponent<UILabel>().text = data.name.ToString();
            item.transform.Find("Icon").GetComponent<UISprite>().spriteName = data.icon.ToString();
            UIEventListener.Get(item).onClick = data.onClick;
            mTopButtons.Add(item);
        }
    }

    private void OnSettingClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_SETTING);
    }

    private void OnAdventureClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_ADVENTURE);
    }

    private void OnRaidClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_WORLDMAP);
    }

    private void OnShopClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_STORE);
    }

    private void OnMenuClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        UIToggle toggle = go.GetComponent<UIToggle>();
        ShowMenus();
    }

    private void HideMenus()
    {
        UIToggle toggle = btnMenu.GetComponent<UIToggle>();
        if (toggle.value)
        {
            toggle.value = false;    
        }
        ShowMenus();
    }

    private void ShowMenus()
    {
        UIToggle toggle = btnMenu.GetComponent<UIToggle>();
        mTopRightGrid.gameObject.SetActive(toggle.value);
        mBottomRight.gameObject.SetActive(!toggle.value);
    }

    private void OnTaskClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_TASK);
    }

    private void OnPartnerClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_PARTNER);
    }

    private void OnPetClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_PET);
    }

    private void OnRelicsClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_RELICS);
    }

    private void OnBagClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_BAG);
    }

    private void OnSkillClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_SKILL);
    }

    private void OnMountClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_MOUNT);
    }



    private void OnHeroClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTWindowManager.Instance.OpenWindow(EWindowID.UI_HEROINFO);
    }

    private void OnJumpClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        CharacterService.Instance.TryJump();
    }

    private void OnAutoClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        if(CharacterManager.Main.IsStealth)
        {
            CharacterManager.Main.Command.Get<CommandStealthLeave>().Do();
        }
        else
        {
            CharacterManager.Main.Command.Get<CommandStealthBegin>().Do();
        }
    }

    private void OnArtClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        CharacterManager.Main.Command.Get<CommandStealthLeave>().Do();
    }

    private void OnPauseClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTLauncher.Instance.LoadScene(GTLauncher.Instance.lastMapId);
    }

    private void OnCastSkillClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        int pos = go.name.ToInt32();
        ESkillPos skillPos = (ESkillPos)pos;
        GTEventCenter.FireEvent(GTEventID.TYPE_CAST_SKILL, skillPos);
        HideMenus();
    }

    private void OnUpdate()
    {
        Character player = CharacterManager.Main;
        if (player == null || player.CacheTransform == null) return;
        OnRefreshSkillItems();
    }

    private void OnUpdateAvatarPower()
    {
        Character player = CharacterManager.Main;
        int maxMP = player.CurrAttr.MaxMP;
        int curMP = player.CurrAttr.MP;
        mBar2.value = curMP / (maxMP * 1f);
        mBarValue2.text = GTTools.Format("{0}/{1}", curMP, maxMP);
    }

    private void OnUpdateAvatarHealth()
    {
        Character player = CharacterManager.Main;
        int maxHP = player.CurrAttr.MaxHP;
        int curHP = player.CurrAttr.HP;
        mBar1.value = curHP / (maxHP * 1f);
        mBarValue1.text = GTTools.Format("{0}/{1}", curHP, maxHP);
    }

    private void OnUpdateAvatarName()
    {
        Character player = CharacterManager.Main;
        mPlayerName.text = player.Name;
    }

    private void OnUpdateAvatarLevel()
    {
        Character player = CharacterManager.Main;
        mPlayerLevel.text = player.Level.ToString();
    }

    private void OnUpdateAvatarExp()
    {
        XCharacter role = RoleModule.Instance.GetCurPlayer();
        int maxExp = ReadCfgRoleLevel.GetDataById(role.Level).RequireExp;
        mBar3.value = (role.CurExp * 1f) / maxExp;
    }

    private void OnUpdateAvatarIcon()
    {
        Character player = CharacterManager.Main;
        DActor db = ReadCfgActor.GetDataById(player.ID);
        mPlayerIcon.spriteName = db.Icon;
    }

    private void OnUpdateAttr()
    {
        OnUpdateAvatarHealth();
        OnUpdateAvatarPower();
    }

    private void OnUpdateThreadTask()
    {

    }

    private void OnUpdateBranchTask()
    {
        string format = GTItemHelper.GetText("[00ffff]【支线】[-]{0}");
        string content = GTItemHelper.GetText("没有发现新任务");
        mTaskContentType2.text = GTTools.Format(format, content);
    }

    private void OnRefreshSkillItems()
    {
        foreach(var current in mSkillBtns)
        {
            ESkillPos pos = current.Key;
            ActSkill skill = CharacterManager.Main.Skill.GetSkill(pos);
            ItemSkill tab = current.Value;
            if (skill == null)
            {
                tab.maskSprite.gameObject.SetActive(true);
                tab.lockSprite.gameObject.SetActive(true);
            }
            else
            {
                tab.lockSprite.gameObject.SetActive(false);
                if(skill.IsCD())
                {
                    tab.maskSprite.gameObject.SetActive(true);
                    tab.maskSprite.fillAmount = skill.GetLeftTime() / skill.CD;
                }
                else
                {
                    tab.maskSprite.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnRefreshSkillIcons()
    {
        foreach (var current in mSkillBtns)
        {
            ESkillPos pos = current.Key;
            ActSkill skill = CharacterManager.Main.Skill.GetSkill(pos);
            ItemSkill tab = current.Value;
            if (skill != null)
            {
                DSkill db = ReadCfgSkill.GetDataById(skill.ID);
                if (db != null)
                {
                    GTItemHelper.ShowTexture(tab.icon, db.Icon);
                }
            }
        }
    }

    private void OnRefreshBuffItems()
    {

    }

    private void OnJoystickMoveEnd(EJoystick move)
    {
        CharacterService.Instance.TryStopJoystick();
    }

    private void OnJoystickMove(EJoystick move)
    {
        float x = move.joystickAxis.x;
        float y = move.joystickAxis.y;
        if (Math.Abs(x) > 0.1f || Math.Abs(y) > 0.1f)
        {
            CharacterService.Instance.TryMoveJoystick(x, y);
            HideMenus();
        }
    }

    private void OnJoystickTouchClick(EJoystick move)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        Character mainPlayer = CharacterManager.Main;
        if (mainPlayer.IsRide)
        {
            CharacterService.Instance.TryLeaveMount();
        }
        else
        {
            CharacterService.Instance.TryStartMount();
        }
    }

    private void OnRefreshPartner()
    {
        XCharacter role = RoleModule.Instance.GetCurPlayer();
        int[]       array    = { role.Partner1, role.Partner2};
        Character[] partners = { CharacterManager.Main.Partner1, CharacterManager.Main.Partner2};
        for (int i = 0; i < array.Length; i++)
        {
            int partnerID = array[i];
            ItemPartner tab = mPartners[i];
            if (partnerID > 0)
            {
                XPartner data = DataDBSPartner.GetDataById(partnerID);
                tab.go.SetActive(true);
                DActor db = ReadCfgActor.GetDataById(partnerID);
                int level = data == null ? db.Level : data.Level;
                tab.heroLevel.text = GTTools.Format("Lv.{0}", level);
                GTItemHelper.ShowQualityText(tab.heroName, db.Name, db.Quality);
                tab.heroIcon.spriteName = db.Icon;
                tab.heroQuality.spriteName = "Q" + (db.Quality);
                Character parnter = partners[i];
                if (parnter == null)
                {
                    tab.heroHPSlider.value = 0;
                    tab.heroHPPercent.text = "0%";
                    GTItemHelper.ShowImageBlack(tab.heroIcon, true);
                    GTItemHelper.ShowImageBlack(tab.heroQuality, true);
                }
                else
                {
                    tab.heroHPSlider.value = (parnter.CurrAttr.HP * 1f) / parnter.CurrAttr.MaxHP;
                    tab.heroHPPercent.text = tab.heroHPSlider.value.ToPercent();
                    GTItemHelper.ShowImageBlack(tab.heroIcon, false);
                    GTItemHelper.ShowImageBlack(tab.heroQuality, false);
                }
            }
            else
            {
                tab.go.SetActive(false);
            }
        }
    }

    private void OnRefreshPartner(int arg1,int arg2)
    {
        OnRefreshPartner();
    }
}