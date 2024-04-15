// MethuselahCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <thread>
#include "Methuselah.h"
#include <wtypes.h>
extern "C" bool __fastcall DllMain(HINSTANCE hinstDLL,
	DWORD fdwReason,
	PVOID lpvReserved) {
	switch (fdwReason) {
	case DLL_PROCESS_ATTACH:
	{
		std::thread t0099(Start);
		t0099.detach();
	}

	case DLL_PROCESS_DETACH:
	{
		break;
	}
	default:
	{
		break;
	}
	}
	return true;
}


