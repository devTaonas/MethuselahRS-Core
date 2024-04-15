#include <Windows.h>
#include <iostream>
#include <cstdio>
#include <string>
#include "Methuselah.h"

bool Start()
{
    // Allocate a new console for the calling process.
    if (!AllocConsole()) {
        // Handle error if needed
    }

    // Redirect standard input, output, and error streams to the console.
    FILE* pCout;
    freopen_s(&pCout, "CONOUT$", "w", stdout);
    freopen_s(&pCout, "CONOUT$", "w", stderr);
    freopen_s(&pCout, "CONIN$", "r", stdin);

    // Optionally, set the title of the console
    SetConsoleTitle(L"METHUSELAH");

    // Your additional initialization code...

    ccout(0xa, "METHUSELAH HAS BEEN INJECTED !");

    while (LocalPlayer == 0)
    {
        cout << hex << "LocalPlayerAddress " << LocalPlayer << endl;
        Sleep(1000);
    }
    while (1)
    {
  //      PlayerCoord();
        ccout(0xa, "");
    }


    return true;
}

#define LAST_STATUS_OFS (0x598 + 0x197 * sizeof(void*))// Offset of 'LastStatus' field in TEB

inline NTSTATUS LastNtStatus() {
    return *(NTSTATUS*)((unsigned char*)NtCurrentTeb() + LAST_STATUS_OFS);
}
NTSTATUS Native_ReadProcessMemoryT(uint64_t lpBaseAddress, LPVOID lpBuffer, size_t nSize, DWORD64* lpBytes /*= nullptr */) {
    auto r = ReadProcessMemory(GameHandle, reinterpret_cast<LPVOID>(lpBaseAddress), lpBuffer, nSize, reinterpret_cast<SIZE_T*>(lpBytes));
    return r != 0 ? STATUS_SUCCESS : LastNtStatus();
}


bool SetLocalPlayer(uint64_t localPlayerAddress)
{
    LocalPlayer = localPlayerAddress;
    cout << "done" << endl;
    return true;
}

void ccout(int color, std::string tex) {

    HANDLE hstdin = GetStdHandle(STD_INPUT_HANDLE);
    HANDLE hstdout = GetStdHandle(STD_OUTPUT_HANDLE);

    // Remember how things were when we started
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    GetConsoleScreenBufferInfo(hstdout, &csbi);

    //first bit is background color, second bit is text color
    SetConsoleTextAttribute(hstdout, color);
    std::cout << tex << std::endl;
    FlushConsoleInputBuffer(hstdin);
    //restore previus color
    SetConsoleTextAttribute(hstdout, csbi.wAttributes);
}



float Readfloat(uint64_t SummPointer) {
    float buffer = 0;
    if (Native_ReadProcessMemoryT(SummPointer, &buffer, sizeof(buffer)) == STATUS_SUCCESS) {
        return buffer;
    }
    return 0.f;
}

int Readint(uint64_t SummPointer) {
    int buffer = 0;
    if (Native_ReadProcessMemoryT(SummPointer, &buffer, sizeof(buffer)) == STATUS_SUCCESS) {
        return buffer;
    }
    return 0;
}