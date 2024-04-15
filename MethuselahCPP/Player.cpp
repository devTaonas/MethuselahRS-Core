#include "Player.h"
#include <cmath>


extern "C" int __declspec(dllexport) SetLocalPlayerAddress(uint64_t blah)
{
	cout << hex << "sent address " << blah << endl;
	SetLocalPlayer(blah);
	return blah;
}

bool PlayerLoggedIn() {
	if (LocalPlayer != 0) {
		int A1 = Readint(LocalPlayer + OFF_OBJALL.gen_active1);
		int A2 = Readint(LocalPlayer + OFF_OBJALL.gen_active2);
		//it goes quickly through here if everthing is fine
		if (A1 > 0 && A1 < 4 && A2 > 0 && A2 < 101) {
			return true;
		}
		else {
			//player can flicker
			for (int i = 0; i < 11; i++) {
				A1 = Readint(LocalPlayer + OFF_OBJALL.gen_active1);
				A2 = Readint(LocalPlayer + OFF_OBJALL.gen_active2);
				Sleep(20);
				if (A1 > 0 && A1 < 4 && A2 > 0 && A2 < 101) {
					return true;
				}
			}
			//falls through
			LocalPlayer = 0;//reset this also
			return false;
		}
	}
	return false;
}

WPOINT PlayerCoord() {
	WPOINT x{ 0,0,0 };
	if (PlayerLoggedIn()) {
		x.x = floor(Readfloat(LocalPlayer + OFF_NPC.npcoff_X_tile) / 512.f);
		x.y = floor(Readfloat(LocalPlayer + OFF_NPC.npcoff_Y_tile) / 512.f);
		x.z = 0;//GetFloorLv_2();
	}
	return x;
}


