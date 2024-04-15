#pragma once
#include "MemStructs.h" 

//jagex stores data

/*
inventory from top to pointer array 0x190, move this time 0x8
*/
struct INTERFACE_Entity
{
    int I_ids = 0x28;             //ids
    int I_offback = 0x30;             //back trough interface chain
    int I_x0 = 0x58;             //x location  
    int I_y0 = I_x0 + 0x4;       //y location   
    int I_x1 = I_x0 + 0x8;       //x size   
    int I_y1 = I_x0 + 0xc;       //y size
    int I_x2 = I_x0 + 0x10;      //x location  
    int I_y2 = I_x0 + 0x14;      //y location  
    int I_x3 = I_x0 + 0x18;      //x size   
    int I_y3 = I_x0 + 0x1c;      //y size  
    int I_x4 = I_x0 + 0x20;      //x total length  
    int I_y4 = I_x0 + 0x24;      //y total lengt
    int I_AbilityEnabled = 0x80;             //something with ability enabled
    int I_textP = 0x90;             //points to text pointer
    int I_text = I_textP + 0x8;    //points to actual text
    int I_00text = I_00textP + 0x8;  //points to actual text
    int I_text2 = I_00textP + 0x20; //points to extra text info. pointer to a pointer to a text
    int I_ActioOP = 0xc8;             //points to OP. 4 byte
    int I_00textP = 0x108;            //text pointer  
    int I_slides = 0x168;            //generic ids//jpeg id
    int I_buffb = I_slides + 0x18;  //specific ids//jpeg id
    int I_itemids3 = 0x178;            //fork/text
    int I_itemids2 = 0x190;            //fork/text
    int I_itemids = 0x198;            //item ids
    int I_itemstack = I_itemids + 8;    //item stack size
    int I_itemids4 = 0x1c0;            //fork/text
    int I_pixels = 0x204;            //scrolling pixels   
    int I_tabID = I_pixels + 8;     //ID of the selected tab
    int I_icon1 = 0x250;            //fork/text
    int I_icon2 = 0x268;            //Mini icons
}inline OFF_INTERFACE;//INTERFACEs

struct OBJ1_Entity
{
    int npcoff_otheraction = 0x70;                 //Text interactivity
    int npcoff_IDU = 0x118;                //unique id
    int npcoff_NameP = 0x140;                //name pointer
    int npcoff_Name = npcoff_NameP + 0x8;   //name plain
    int npcoff_model_2 = 0x198;                //models pointer to/second data blob  
    int npcoff_IAct = 0x2a8;                //shows entity which is currently interacted with
    int npcoff_X_tile = 0x2d8;                //x
    int npcoff_Y_tile = 0x2e0;                //y
    int npcoff_model_1 = 0xA08;                //models pointer 1/main data blob  
    int npcoff_Move = 0xA40;                //move bool
    int npcoff_Anim = 0xaf8;                //anim       
    int npcoff_Stance = 0xF88;                //Player stance, depends on weapons
    int npcoff_ID = 0x10B0;               //id       
    int npcoff_other = npcoff_ID + 0x8;      //points to other array where action,name and id are stored
    int npcoff_Life = 0x1174;               //life    
    int npcoff_MaxLife = npcoff_Life + 0x1C;   //max life
    int npcoff_CMB = npcoff_MaxLife + 0x10;//combat lv
}inline OFF_NPC;//NPCs/players, type 1/2

struct OBJ0_Entity
{
    int aoofftx2 = 0xa8;  //first action text
    int aooff_ids = 0x110; //ids  
    int aoofftx0 = 0x128; //to the other text array//several actions for obj in text there   
    int aoofftx1 = 0x200; //to the text in other array//text end next line  
    int aooff_ids2 = 0x210; //ids 2
    int aooff_bool = 0x175; //choped down boolean
    int aoofftileX = 0x1D4; //tiles
}inline OFF_OBJ0;//Objects, type 0

struct OBJ12_Entity
{
    int dooff_ids2 = 0x40;  //ids in other array   
    int doofftx = 0x60;  //to the text in other array
    int doofftileX = 0x104; //tiles
    int dooff_ids = 0x110; //ids
    int dooff_bool = 0x15A; //object interactable bool? Archelogy
}inline OFF_OBJ12;//Objects, type 12

struct OBJG_Entity
{
    int gotherid = 0x20;  //other arr id 
    int gotheram = 0x24;  //other arr am
    int goff333 = 0x100; //first id
    int goff33 = 0x128; //to id
}inline OFF_OBJG;//Grounditems, type 3

struct OBJALL_Entity
{
    int gen_off_x1 = 0x30;             //Float x, bottom
    int gen_off_z1 = gen_off_x1 + 4;   //Float z, bottom  
    int gen_off_y1 = gen_off_x1 + 8;   //Float y, bottom
    int gen_off_x2 = 0x50;             //Float x, upper   
    int gen_off_z2 = gen_off_x2 + 4;   //Float z, upper 
    int gen_off_y2 = gen_off_x2 + 8;   //Float y, upper  
    int gen_off_x3 = 0x80;             //Float x, middle   
    int gen_off_z3 = gen_off_x3 + 4;   //Float z, middle
    int gen_off_y3 = gen_off_x3 + 8;   //Float y, middle      
    int all_object = 0x1d0;            //to object   
    int all_sc_partial = 0x1dc;            //screen timer partial
    int all_sc_full = 0x1fd;            //on screen fully bool
    //at other//
    int gen_active1 = 0x8;              //activ1
    int gen_active2 = 0xc;              //activ2   
    int all_back = 0x58;             //backto all
    int allofftype = 0x60;             //to type
    int allfloor = allofftype + 0x4; //at floor
    int gen_off_xy = 0x70;             //Pixels location start   
    int gen_off_xm = 0x74;             //Pixels xmid  
    int gen_off_ym = 0x74 + 4;         //Pixels ymid    
    int gen_off_xysize = 0x74 + 8;         //Pixels size
}inline OFF_OBJALL;//All objects reserve

struct OBJWALL_Entity
{
    int GT_big = 0x108; //from main
    int GT_step = 0xc0;
    int GT_id = 0x54;
    int GT_x = 0x14;
    int GT_y = GT_x + 0x4;
    int GT_x_size = 0x28;
    int GT_y_size = GT_x_size + 0x4;
}inline OFF_OBJWALL;//walls, type 8

struct OBJEXTRA_Entity
{
    int pro_id = 0xF0;  //projectile id  
    int other_id = 0x104; //id of clue scroll pulse type 4
}inline OFF_OBJEXTRA;//type "any"

struct MAIN_M_STRUCT
{
    uint64_t P_toMainData = 0;                   //contains main pointer
    uint64_t off_to_status = 0x19DA0;             //to the status of the game
    uint64_t off_to_data = off_to_status + 0x8; //to player data pointer, also name there
    uint64_t off_other_member = 0x28;                //member bool
    uint64_t off_other_server_index = 0x48;                //local player index//unique id
    uint64_t off_other_name = 0x68;                //from other data to name

}inline MEM_Resrv0;//



//{ 1477, 837, -1, -1 }, { 1477,840,-1,837 }, { 1477,842,-1,840 }, { 1477,842,0,842 
//											{ { 1477,871,-1,-1,0 }, { 1477,874,-1,871,0 }, { 1477,876,-1,874,0 }, { 1477,876,0,876,0 } } change +33
const std::vector<InterfaceComp5> MENU_side{ { 1477,871,-1,-1,0 }, { 1477,874,-1,871,0 }, { 1477,876,-1,874,0 }, { 1477,876, 0,876,0 } };

//{ 1477, 837, -1, -1 }, { 0x1477,0x838 ,0xFFFF ,0x837 }
const std::vector<InterfaceComp5> MENU_choose{ { 1477, 871, -1, -1,0 }, { 1477,872 ,-1 ,871,0 } };

//looking for better spot...
//viewmatrix
constexpr int CompassSpot22 = 0x80;
//direct angles, other 0x20
constexpr int Compass2cosAngle2 = 0x0;
constexpr int Compass2sinAngle2 = Compass2cosAngle2 + 0x10;