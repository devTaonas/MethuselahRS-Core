#include "Load_std_headers.h"
#include <stdint.h>
#include <memoryapi.h>
#include <ntstatus.h>
#include "MemOffsets.h"
#include <synchapi.h>

using namespace std;

inline uint64_t LocalPlayer = 0;
inline HANDLE GameHandle{};

bool Start();

void ccout(int color, std::string tex);


int Readint(uint64_t); // 4 byte

float Readfloat(uint64_t SummPointer); // float

bool SetLocalPlayer(uint64_t localPlayerAddress);

NTSTATUS Native_ReadProcessMemoryT(uint64_t lpBaseAddress, LPVOID lpBuffer, size_t nSize, DWORD64* lpBytes = nullptr);







