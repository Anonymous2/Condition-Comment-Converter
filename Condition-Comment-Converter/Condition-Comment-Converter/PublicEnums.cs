using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condition_Comment_Converter
{
    public enum ConditionSourceType
    {
        CONDITION_SOURCE_TYPE_NONE                           = 0,
        CONDITION_SOURCE_TYPE_CREATURE_LOOT_TEMPLATE         = 1,
        CONDITION_SOURCE_TYPE_DISENCHANT_LOOT_TEMPLATE       = 2,
        CONDITION_SOURCE_TYPE_FISHING_LOOT_TEMPLATE          = 3,
        CONDITION_SOURCE_TYPE_GAMEOBJECT_LOOT_TEMPLATE       = 4,
        CONDITION_SOURCE_TYPE_ITEM_LOOT_TEMPLATE             = 5,
        CONDITION_SOURCE_TYPE_MAIL_LOOT_TEMPLATE             = 6,
        CONDITION_SOURCE_TYPE_MILLING_LOOT_TEMPLATE          = 7,
        CONDITION_SOURCE_TYPE_PICKPOCKETING_LOOT_TEMPLATE    = 8,
        CONDITION_SOURCE_TYPE_PROSPECTING_LOOT_TEMPLATE      = 9,
        CONDITION_SOURCE_TYPE_REFERENCE_LOOT_TEMPLATE        = 10,
        CONDITION_SOURCE_TYPE_SKINNING_LOOT_TEMPLATE         = 11,
        CONDITION_SOURCE_TYPE_SPELL_LOOT_TEMPLATE            = 12,
        CONDITION_SOURCE_TYPE_SPELL_IMPLICIT_TARGET          = 13,
        CONDITION_SOURCE_TYPE_GOSSIP_MENU                    = 14,
        CONDITION_SOURCE_TYPE_GOSSIP_MENU_OPTION             = 15,
        CONDITION_SOURCE_TYPE_CREATURE_TEMPLATE_VEHICLE      = 16,
        CONDITION_SOURCE_TYPE_SPELL                          = 17,
        CONDITION_SOURCE_TYPE_SPELL_CLICK_EVENT              = 18,
        CONDITION_SOURCE_TYPE_QUEST_ACCEPT                   = 19,
        CONDITION_SOURCE_TYPE_QUEST_SHOW_MARK                = 20,
        CONDITION_SOURCE_TYPE_VEHICLE_SPELL                  = 21,
        CONDITION_SOURCE_TYPE_SMART_EVENT                    = 22,
        CONDITION_SOURCE_TYPE_NPC_VENDOR                     = 23,
        CONDITION_SOURCE_TYPE_SPELL_PROC                     = 24,
        CONDITION_SOURCE_TYPE_PHASE_DEFINITION               = 25, // only 4.3.4
        CONDITION_SOURCE_TYPE_MAX                            = 26  // MAX
    }

    public enum ConditionTypes
    {                                                           // value1           value2         value3
        CONDITION_NONE                  = 0,                    // 0                0              0                  always true
        CONDITION_AURA                  = 1,                    // spell_id         effindex       use target?        true if player (or target, if value3) has aura of spell_id with effect effindex
        CONDITION_ITEM                  = 2,                    // item_id          count          bank               true if has #count of item_ids (if 'bank' is set it searches in bank slots too)
        CONDITION_ITEM_EQUIPPED         = 3,                    // item_id          0              0                  true if has item_id equipped
        CONDITION_ZONEID                = 4,                    // zone_id          0              0                  true if in zone_id
        CONDITION_REPUTATION_RANK       = 5,                    // faction_id       rankMask       0                  true if has min_rank for faction_id
        CONDITION_TEAM                  = 6,                    // player_team      0,             0                  469 - Alliance, 67 - Horde)
        CONDITION_SKILL                 = 7,                    // skill_id         skill_value    0                  true if has skill_value for skill_id
        CONDITION_QUESTREWARDED         = 8,                    // quest_id         0              0                  true if quest_id was rewarded before
        CONDITION_QUESTTAKEN            = 9,                    // quest_id         0,             0                  true while quest active
        CONDITION_DRUNKENSTATE          = 10,                   // DrunkenState     0,             0                  true if player is drunk enough
        CONDITION_WORLD_STATE           = 11,                   // index            value          0                  true if world has the value for the index
        CONDITION_ACTIVE_EVENT          = 12,                   // event_id         0              0                  true if event is active
        CONDITION_INSTANCE_INFO         = 13,                   // entry            data           type               true if the instance info defined by type (enum InstanceInfo) equals data.
        CONDITION_QUEST_NONE            = 14,                   // quest_id         0              0                  true if doesn't have quest saved
        CONDITION_CLASS                 = 15,                   // class            0              0                  true if player's class is equal to class
        CONDITION_RACE                  = 16,                   // race             0              0                  true if player's race is equal to race
        CONDITION_ACHIEVEMENT           = 17,                   // achievement_id   0              0                  true if achievement is complete
        CONDITION_TITLE                 = 18,                   // title id         0              0                  true if player has title
        CONDITION_SPAWNMASK             = 19,                   // spawnMask        0              0                  true if in spawnMask
        CONDITION_GENDER                = 20,                   // gender           0              0                  true if player's gender is equal to gender
        CONDITION_UNIT_STATE            = 21,                   // unitState        0              0                  true if unit has unitState
        CONDITION_MAPID                 = 22,                   // map_id           0              0                  true if in map_id
        CONDITION_AREAID                = 23,                   // area_id          0              0                  true if in area_id
        CONDITION_CREATURE_TYPE         = 24,                   // cinfo.type       0              0                  true if creature_template.type = value1
        CONDITION_SPELL                 = 25,                   // spell_id         0              0                  true if player has learned spell
        CONDITION_PHASEMASK             = 26,                   // phasemask        0              0                  true if object is in phasemask
        CONDITION_LEVEL                 = 27,                   // level            ComparisonType 0                  true if unit's level is equal to param1 (param2 can modify the statement)
        CONDITION_QUEST_COMPLETE        = 28,                   // quest_id         0              0                  true if player has quest_id with all objectives complete, but not yet rewarded
        CONDITION_NEAR_CREATURE         = 29,                   // creature entry   distance       0                  true if there is a creature of entry in range
        CONDITION_NEAR_GAMEOBJECT       = 30,                   // gameobject entry distance       0                  true if there is a gameobject of entry in range
        CONDITION_OBJECT_ENTRY          = 31,                   // TypeID           entry          0                  true if object is type TypeID and the entry is 0 or matches entry of the object
        CONDITION_TYPE_MASK             = 32,                   // TypeMask         0              0                  true if object is type object's TypeMask matches provided TypeMask
        CONDITION_RELATION_TO           = 33,                   // ConditionTarget  RelationType   0                  true if object is in given relation with object specified by ConditionTarget
        CONDITION_REACTION_TO           = 34,                   // ConditionTarget  rankMask       0                  true if object's reaction matches rankMask object specified by ConditionTarget
        CONDITION_DISTANCE_TO           = 35,                   // ConditionTarget  distance       ComparisonType     true if object and ConditionTarget are within distance given by parameters
        CONDITION_ALIVE                 = 36,                   // 0                0              0                  true if unit is alive
        CONDITION_HP_VAL                = 37,                   // hpVal            ComparisonType 0                  true if unit's hp matches given value
        CONDITION_HP_PCT                = 38,                   // hpPct            ComparisonType 0                  true if unit's hp matches given pct
        CONDITION_MAX                   = 39                    // MAX
    }
}
