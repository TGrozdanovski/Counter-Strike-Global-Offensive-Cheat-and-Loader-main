#include "CVar.h"

// CREDITS TO FRIEND, NOT ME

CVarSystem g_pCvar;

void CVarSystem::Init()
{
	g_pCvar.addCvar("aimbot_active", 0, false);
	g_pCvar.addCvar("aimbot_key", 1, false);

	g_pCvar.addCvar("aimbot_fov", 6, false);
	g_pCvar.addCvar("aimbot_weapon_group", 0, false);
	g_pCvar.addCvar("aimbot_distance_fov", 1, false);
	g_pCvar.addCvar("aimbot_spot", 0, false);
	g_pCvar.addCvar("aimbot_smooth", 10, false);
	g_pCvar.addCvar("aimbot_rcs", 1, false);
	g_pCvar.addCvar("aimbot_humanize", 1, false);
	g_pCvar.addCvar("aimbot_silent", 0, false);	

	g_pCvar.addCvar("pistol_aimbot_key", 1, false);

	g_pCvar.addCvar("pistol_aimbot_spot", 0, false);
	g_pCvar.addCvar("pistol_aimbot_smooth", 8, false);
	g_pCvar.addCvar("pistol_aimbot_fov", 4, false);
	g_pCvar.addCvar("pistol_aimbot_rcs", 4, false);
	
	g_pCvar.addCvar("rifle_aimbot_key", 1, false);

	g_pCvar.addCvar("rifle_aimbot_spot", 0, false);
	g_pCvar.addCvar("rifle_aimbot_smooth", 8, false);
	g_pCvar.addCvar("rifle_aimbot_fov", 4, false);	
	g_pCvar.addCvar("rifle_aimbot_rcs", 4, false);

	g_pCvar.addCvar("sniper_aimbot_key", 1, false);

	g_pCvar.addCvar("sniper_aimbot_spot", 0, false);
	g_pCvar.addCvar("sniper_aimbot_smooth", 8, false);
	g_pCvar.addCvar("sniper_aimbot_fov", 4, false);	
	g_pCvar.addCvar("sniper_aimbot_rcs", 4, false);			

	g_pCvar.addCvar("trigger_active", 0, false);
	g_pCvar.addCvar("trigger_key", 6, false);
	g_pCvar.addCvar("trigger_overshoot", 0, false);	

}

bool CVarSystem::IsActive()
{
	if (!bActive)
		return false;
	else
		return true;
}

char* stringToLower(char *string)
{
	int len = strlen(string);

	for (int i = 0; i < len; i++)
	{
		if (string[i] >= 'A' && string[i] <= 'Z')
			string[i] += 32;
	}

	return string;
}

bool bIsDigitInString(std::string pszString)
{
	for (int ax = 0; ax <= 9; ax++)
	{
		char buf[MAX_PATH];
		_snprintf(buf, (size_t)255, "%d", ax);

		if (strstr(pszString.c_str(), buf))
			return true;
	}

	return false;
}

void CVarSystem::addCvar(char *szName, int iValue, bool bStyle)
{
	CRetVar buf = CRetVar(szName, iValue, bStyle);
	vars.push_back(buf);
}

int CVarSystem::getValue(const char* szName)
{
	for (int ax = 0; ax < vars.size(); ax++)
	{
		if (vars[ax].szName == szName)
			return vars[ax].iValue;
	}

	return 0;
}

void CVarSystem::setValue(const char* szName, int szValue)
{
	for (int ax = 0; ax < vars.size(); ax++)
	{
		if (vars[ax].szName == szName)
			vars[ax].iValue = szValue;
	}
}



