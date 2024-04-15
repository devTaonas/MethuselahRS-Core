#include "Methuselah.h"

bool DoAction_Tile(WPOINT normal_tile) {

	//WPOINT p = PlayerCoord();
	//if (normal_tile.z == 0) { normal_tile.z = PlayerCoordfloat().z; }
	//if (abs(p.x - normal_tile.x) < 36) {
	//	if (abs(p.y - normal_tile.y) < 36) {
	//		WPOINT p3 = Math_W2Sv2W({ normal_tile.x * 512.f, normal_tile.y * 512.f, normal_tile.z * 512.f });
	//		if (p3.x > 4000 && p3.x < 1) { p3.x = Math_RandomNumber(GetRsResolution2().x); }
	//		if (p3.y > 4000 && p3.y < 1) { p3.x = Math_RandomNumber(GetRsResolution2().y); }
	//		if (Doaction_paint) {
	//			IG_answer* P_data = new IG_answer{};
	//			P_data->box_name = "Tile" + std::to_string(normal_tile.x) + std::to_string(normal_tile.y);
	//			P_data->box_start = normal_tile;
	//			P_data->how_many_sec = Doaction_paint_alive;
	//			P_data->temp_created = true;
	//			IG::DrawCircleTimeOut(1, P_data);
	//		}
	//		Push_D_A_Q(Fake_action_call, 100, -1, OFF_ACT::Walk_route, 0, normal_tile.x, normal_tile.y, 0, p3.x, p3.y);
	//		std::cout << "DoAction_Tile:Ground" << std::endl;
	//		return true;
	//	}
	//}
	//if (abs(p.x - normal_tile.x) < 74) {
	//	if (abs(p.y - normal_tile.y) < 74) {
	//		WPOINT p3 = Math_W2Sv2W({ normal_tile.x * 512.f, normal_tile.y * 512.f,normal_tile.z * 512.f });
	//		if (p3.x > 4000 && p3.x < 1) { p3.x = Math_RandomNumber(GetRsResolution2().x); }
	//		if (p3.y > 4000 && p3.y < 1) { p3.x = Math_RandomNumber(GetRsResolution2().y); }
	//		if (Doaction_paint) {
	//			IG_answer* P_data = new IG_answer{};
	//			P_data->box_name = "Tile" + std::to_string(normal_tile.x) + std::to_string(normal_tile.y);
	//			P_data->box_start = normal_tile;
	//			P_data->how_many_sec = Doaction_paint_alive;
	//			P_data->temp_created = true;
	//			IG::DrawCircleTimeOut(1, P_data);
	//		}
	//		Push_D_A_Q(Fake_action_call, 100, -1, OFF_ACT::Walk_route, 1, normal_tile.x, normal_tile.y, 0, p3.x, p3.y);
	//		std::cout << "DoAction_Tile:Map" << std::endl;
	//		return true;
	//	}
	//}
	//std::cout << "DoAction_Tile:Tile out of range" << std::endl;
	return false;
}