#pragma once
#define UMDF_USING_NTSTATUS //use NT STATUS values
#include <Windows.h> //has some standard stuff included
#include <bitset>
#include <random>
#include <fstream>
#include <filesystem>
#include <cstdlib>
#include <psapi.h>
#include <ntstatus.h>
#include <iostream>
#include <mutex>
#include <thread>

#pragma comment(lib, "Version.lib")
#include <mmsystem.h>
#pragma comment(lib, "winmm.lib")
