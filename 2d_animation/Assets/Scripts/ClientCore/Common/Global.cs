/********************************************************************
	Copyright(c) 2014-2015 QeFun Corporation. All rights reserved.
	created:	19-8-2014   14:21
	filename: 	Assets\Core\Common\Globals.cs
	file base:	Globals
	author:		zourongchun

	description: All global constants and variables list below.
*********************************************************************/

namespace ClientCore {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;

    /// <summary>
    /// 层级
    /// </summary>
    public class LayerManager {
        public const int Default = 0;
        public const int UI = 5;
        public const int BattleSelf = 8;
        public const int BattleEnemy = 9;
        public const int ArmsSelf = 10;
        public const int ArmsEnemy = 11;
        public const int Block = 12;
        public const int UI3DRole = 13;
        public const int Highlight = 14;
        public const int UIHighlight = 15;
        public const int UITop = 16;
        public const int TowerSelf = 17;
        public const int TowerEnemy = 18;
        public const int HallBackground = 19;
    }

    /// <summary>
    /// 全局常量
    /// </summary>
    public static class Global {
        //region of global constants
        //////////////////////////////////////////////////////////////
        public const int INVALID_INT = -1;
        public const uint INVALID_UINT = 0xFFFFFFFF;
        public const long INVALID_LONG = -1;
        public const ulong INVALID_ULONG = 0xFFFFFFFFFFFFFFFF;
        public const float INVALID_FLOAT = -1f;
        public const int INT_ZERO = 0;
        public const int INT_ONE = 1;

        public const int HERO_AUTO_AI_GROUP_ID = 100001;
        public const int MONSTER_AI_GROUP_ID = 100002;
        public const int TOWER_AI_GROUP_ID = 100003;

        public const int SELF_INIT_CAST_TABLE_ID = 1001;

        public const string SCENE_LOADER = "Loader";
        public const string SCENE_LOGIN = "Login";

        public const int SCENETABLEID_LOGIN = 10001;
        public const int SCENETABLEID_BATTLE1 = 10002;
        public const int SCENETABLEID_HALL = 10003;
        public const int HERO_ID_MAX = 99999;

        public const int PLAYER_MAX_HERO_STARS_COUNT = 5;

        public const int MAX_PLAYER_LEVEL = 90;

        public const float TurnSmoothing = 0.01f;   // 用于玩家平滑转向的值 

        //transform最大层级
        public const int MAX_TRANSFORM_HIERARCHY = 5;

        //副本最大星星数量
        public const int MAX_RAID_STAR_COUNT = 3;

        /// <summary>
        /// 最大上场英雄个数
        /// </summary>
        public const int MAXHEROCOUNT = 3;
        /// <summary>
        /// 最大上场投射物个数
        /// </summary>
        public const int MAXBULLETCOUNT = 4;

        // 随机数生成器
        public static readonly System.Random Srand = new System.Random();

        public static readonly Vector3 INVALID_POSITION = new Vector3(-1.0f, -1.0f, -1.0f);
        public static readonly Quaternion BORN_ROTATION = new Quaternion(-1.0f, -1.0f, -1.0f, -1.0f);

        // temp Player Name
        public static string tempPlayerName;

        public const int TRUE = 1;
        public const int FALSE = 0;

        //生效坐标
        public static readonly Vector3 UNFROZEN_POSITION_X = new Vector3(1, 0, 0);      //未冻结X坐标
        public static readonly Vector3 UNFROZEN_POSITION_Y = new Vector3(0, 1, 0);      //未冻结Y坐标
        public static readonly Vector3 UNFROZEN_POSITION_Z = new Vector3(0, 0, 1);      //未冻结Z坐标

        public const int FPS = 30; //帧率

        //最大等级
        public static int MAX_LEVEL = 100;

        public static readonly float COLOR_TRANSFORM = 1f / 255f;

        //--------------------------------角色换色相关定义--------------------------------------------

        public const string TOWER_DOOR_NAME = "castle_08";

        public const string ROLE_SHADER_NAME_DEFAULT = "Self-Illumin/Diffuse";
        public const string ROLE_SHADER_NAME_GRAY = "Custom/Illumin-Rim-Gray";

        public const string ROLE_SHADER_NAME_PATHTITLE = "Custom/";
        public const string ROLE_SHADER_NAME_ILLUMIN = "Illumin-";
        public const string ROLE_SHADER_NAME_OUTLINE = "-OutLine";
        public const string ROLE_SHADER_NAME_ALPHA = "-Alpha";

        public const string ROLE_SHADER_PRO_NAME_COLOR = "_Color";

        public const string ROLE_SHADER_PRO_NAME_EMISSION = "_EmissionLM";

        public const string ROLE_SHADER_PRO_NAME_RIMCOLOR = "_RimColor";
        public const string ROLE_SHADER_PRO_NAME_RIMPOWER = "_RimPower";
        public const string ROLE_SHADER_PRO_NAME_COLOR2 = "_Color2";
        public const string ROLE_SHADER_PRO_NAME_INTENSITY = "_Intensity";

        public const string ROLE_SHADER_PRO_NAME_SPECCOLOR = "_SpecColor";
        public const string ROLE_SHADER_PRO_NAME_SHININESS = "_Shininess";

        public const string ROLE_SHADER_PRO_NAME_OUTLINE = "_Outline";
        public const string ROLE_SHADER_PRO_NAME_OUTLINECOLOR = "_OutlineColor";

        public const string ROLE_SHADER_PRO_NAME_ALPHA = "_Alpha";

        public static readonly Color TEAM_SELF = new Color(249f / 255f, 54f / 255f, 6f / 255f, 0f);
        public static readonly Color TEAM_ENEMY = new Color(0f, 179f / 255f, 247f / 255f, 0f);
        public static readonly Color TEAM_DEFAULT = new Color(249f / 255f, 86f / 255f, 6f / 255f, 0f);

        public static readonly Color ROLE_DEFAULT_COLOR = new Color(201f / 255f, 201f / 255f, 201f / 255f, 1f);
        public static readonly Color ROLE_DEFAULT_SPECCOLOR = new Color(111f / 255f, 125f / 255f, 144f / 255f, 1f);

        public static readonly Color ROLE_DEFAULT_OUTLINECOLOR = new Color(0f, 0f, 0f, 1f);

        public const float ROLE_SHADER_PRO_EMISSION = 1f;

        public const float ROLE_SHADER_PRO_INTENSITY = 1f;
        public const float ROLE_SHADER_PRO_RIMPOWER = 2f;
        public const float ROLE_SHADER_PRO_SHININESS = 0.27f;

        public const float ROLE_SHADER_PRO_OUTLINE_DEFAULT = 0f;
        public const float ROLE_SHADER_PRO_OUTLINE_BATTLE = 0.03f;

        public const float ROLE_SHADER_PRO_ALPHA_DEFAULT = 1f;
        //------------------------------------------------------------------------------------------

        //--------------------------------物品相关定义------------------------------------------------

        public static readonly int ID_MAXLENGTH_BULLET = 4; //投射物id最大位数
        public static readonly int ID_MAXLENGTH_HERO = 6;   //英雄id最大位数
        public static readonly int ID_MINLENGTH_ITEM = 7;   //物品id最小位数

        public const string ATLAS_NAME_BULLET = "bulleticon";   //投射物图标图集名称
        public const string ATLAS_NAME_HERO = "Headicon";       //英雄图标图集名称
        public const string ATLAS_NAME_ITEM = "Itemicon";       //物品图标图集名称

        //------------------------------------------------------------------------------------------

        //---------------------------------脚本名称，脚本函数------------------------------------------

        public static readonly string SCRIPT_NAME_SCENEGAMEOBJECTSCRIPT = "SceneGameObjectUserDataScript";
        public static readonly string SCRIPT_NAME_WWINDOW = "WWindow";
        public static readonly string SCRIPT_NAME_WPANEL = "WPanel";

        //------------------------------------------------------------------------------------------

        // Icon Quality
        public static readonly string[] ICON_QUALITY = { "IconFrame_0", "IconFrame_1", "IconFrame_2", "IconFrame_3", "IconFrame_4" };
        // 碎片品质
        public static readonly string[] CHIP_QUALITY = { "IconFrame_Deb_0", "IconFrame_Deb_1", "IconFrame_Deb_2", "IconFrame_Deb_3", "IconFrame_Deb_4" };
        //货币图标
        public static readonly string[] MONEY_ICON = { "Sign_Gold", "Sign_Diamond", "Sign_PVP", "Sign_Play", "Sign_Long", "Sign_Sociaty" };
        //英雄标签图标
        public static readonly string[] HERO_TAG_ICON = { "Sign_Big_H_1", "Sign_Big_H_2", "Sign_Big_H_3" };
        //投射物品质
        public static readonly string[] BULLET_ICON_QUALITY = { "BulletFrame_1", "BulletFrame_2", "BulletFrame_2_1", "BulletFrame_3", 
                                                                  "BulletFrame_3_1", "BulletFrame_3_2", "BulletFrame_4", "BulletFrame_4_1", "BulletFrame_4_2", "BulletFrame_4_3", "BulletFrame_5" };
        //投射物种类
        public static readonly string[] BULLET_ICON_TYPE = { "BulletSign_1", "BulletSign_2", "BulletSign_3", "BulletSign_4" };
        
        public static readonly Color[] MONEY_COLOR = { 
            new Color(250f / 255f, 222f / 255f, 0f, 1f), new Color(0, 222f / 255f, 250f / 255f, 1f),
            new Color(0, 222f / 255f, 250f / 255f, 1f), new Color(0, 222f / 255f, 250f / 255f, 1f),
            new Color(0, 222f / 255f, 250f / 255f, 1f),new Color(0, 222f / 255f, 250f / 255f, 1f)
            };
        // 金钱足够颜色
        public static readonly Color MONEY_ENOUGH_COLOR = Color.white;
        // 金钱不够颜色
        public static readonly Color MONEY_NOT_ENOUGH_COLOR = Color.red;
        // 经验小图标
        public static readonly string EXP_ICON = "Sign_EXP";
        // 体力小图标
        public static readonly string ENERGY_ICON = "Sign_Power";
        // 城主微记
        public static readonly string CASTLE_TECH_ICON = "Sign_Castle";
        // 扫荡卷
        public static readonly string SWEEP_ICON = "Sign_Sweep";
        // 锤子
        public static readonly string HAMMER_ICON = "Sign_Hammer";
        // 经验瓶
        public static readonly string BOTTLE_ICON = "Sign_Bottle";

        // 好友上限
        public static readonly int MAX_FRIEND_COUNT = 60;
        // 好友申请列表最大上限
        public static readonly int MAX_FRIEND_REQ_COUNT = 20;
        // 战斗统计数据最大个数
        public const int MAX_BATTLE_HERO_STAT_COUNT = 3;
        public const int MAX_BATTLE_BULLET_STAT_COUNT = 4;
        public const int MAX_BATTLE_STAT_TEAM_COUNT = 2;
        public const int MAX_BATTLE_NPC_COUNT = 12;

        // WIFI PK 场景
        public const int WIFI_PK_SCENEID = 90001;
        // 天梯争霸场景id
        public const int ARENA_SCENEID = 90000;
        //远征场景
        public const int EXPEDITION_SCENEID = 91000;
        // 好友切磋场景
        public const int FRIEND_CHALLENGE_SCENEID = 90100;
        // 抢矿
        public const int FIGHT_MINE_SCENEID = 90200;

        //远征默认的血量和能量百分比，精确到小数点后一位，用整数表示
        public const int EXPEDITION_DEFAULT_PERCENT = 1000;

        // 新手引导 第一个
        public const int GUIDE_FIRST_BATTLE_TRIGGER_ID = 1;
        public static readonly int[] GUIDE_BATTLE_TRIGGER_IDS = { /*3, 3, 4*/ };

        public const int MAX_GLOBAL_BUFF_COUNT = 15;

        public const int MAX_DEBUG_OBJECT_ATTR_COUNT = 10; //属性数量

        // IP Length
        public const int MAX_IP_LENGTH = 16;

        public const float BUFF_EFFECT_WIDTH = 1f;

        //前五关的场景ID
        public const int RAID_SCENEID_0 = 11001;
        public const int RAID_SCENEID_1 = 11002;
        public const int RAID_SCENEID_2 = 11003;
        public const int RAID_SCENEID_3 = 11004;
        public const int RAID_SCENEID_4 = 11005;

        // 被动/主动技能框
        public const string ACTIVE_SKILL_FRAME = "Skill_Frame_1";
        public const string PASSIVE_SKILL_FRAME = "Skill_Frame_0";

        // 金钱显示边界
        public const int MONEY_SHOW_BORDER = 1000000;

        // 神秘商店持续时间
        public const int MYSTERY_SHOP_DURATION = 3600;

        /// <summary>
        /// 屏蔽行关键词
        /// </summary>
        public const char FILE_SHIELDING_LINE_KEYWORD_CHAR = '#';
        /// <summary>
        /// tab
        /// </summary>
        public const char TAB_CHAR = '\t';
        /// <summary>
        /// line end
        /// </summary>
        public const string ENTER_STR = "\n";

        /// <summary>
        /// 默认的地图名字
        /// </summary>
        public const string NORMAL_CHAPTER_ROAD_NAME = "11001";

        /// <summary>
        /// 安卓最低内存大小
        /// </summary>
        public const int LIMIT_SYSTEM_ANDROID_MEMORY_SIZE = 1024;
        /// <summary>
        /// ios最低内存大小
        /// </summary>
        public const int LIMIT_SYSTEM_IOS_MEMORY_SIZE = 1024;
        public const int LIMIT_PROCESSOR_COUNT = 2;

        public const string LOGIN_VERIFY_PKEY = "qfyxys2016";

        public const int SERVER_LIST_ZONE_VALUE = 1000;

        //技能书物品ITEMID
        public const int SKILL_BOOK_ITEM_ID = 1005003;
    }
}
