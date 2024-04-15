#pragma once
#include <cstdint>
#include <string>
#include <vector>

//we store data

// ground items info
struct GroundItems {
	int Id = 0;
	std::string Name{};
	bool noted = false;
};

// text choices
struct ReturnText {
	std::string Name{};
	int Nr = 0;

	ReturnText() = default;
	ReturnText(std::string _Name, int _Nr) : Name{ _Name }, Nr{ _Nr } {}
};

//
struct ChatTexts {
	std::string name{};
	std::string text{};
	std::string text_extra1{};
	std::string text_extra2{};
	uint64_t mem_loc = 0;
	uint64_t pc_time_stamp = 0;
	size_t pos_found = 0;
	uint64_t time_total = 0;
};

// player name info
struct NameData {
	std::string Name{};
	std::string Password{};
	std::string Pin{};
};

// for keyboard
struct KeyboardKeys {
	unsigned char virtual_keys = 0;
	unsigned char scan_keys = 0;
	bool extended = 0;
	unsigned char ascii = 0;
};

// for coordinates
struct FFPOINT {
	float x = 0.f;
	float y = 0.f;
	float z = 0.f;

	FFPOINT() = default;
	FFPOINT(float _x, float _y, float _z) : x{ _x }, y{ _y }, z{ _z } {}
};

// for coordinates, int size point
struct WPOINT {
	int x = 0;
	int y = 0;
	int z = 0;

	WPOINT() = default;
	WPOINT(int _x, int _y, int _z) : x{ _x }, y{ _y }, z{ _z } {}
	WPOINT(const FFPOINT& f) : x(f.x), y(f.y), z(f.z) {}

	operator FFPOINT() const { return FFPOINT(x, y, z); }
};

// for 4 edges, int size
struct QWPOINT {
	int bottom = 0;
	int left = 0;
	int right = 0;
	int top = 0;

	QWPOINT() = default;
	QWPOINT(int _bottom, int _left, int _right, int _top) :
		bottom{ _bottom }, left{ _left }, right{ _right }, top{ _top } {}
};

struct InterfaceComp5 {
	int id1 = 0;
	int id2 = 0;
	int id3 = 0;
	int id4 = 0;
	uint64_t memloc = 0;

	InterfaceComp5() = default;
	InterfaceComp5(int _id1, int _id2, int _id3, int _id4, uint64_t _memloc) :
		id1{ _id1 }, id2{ _id2 }, id3{ _id3 }, id4{ _id4 }, memloc{ _memloc } {}
};
