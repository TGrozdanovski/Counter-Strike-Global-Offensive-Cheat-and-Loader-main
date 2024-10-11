#include "CreateMove.h"

void __fastcall CreateMoveHook(void* thisptr, int edx, int sequence_number, float input_sample_frametime, bool active)
{
	typedef void(__fastcall *original)(void*, int, int, float, bool);
	g_phkCreateMove->GetMethod<original>(22)(thisptr, edx, sequence_number, input_sample_frametime, active);

	CInput::CUserCmd* pCmd = g_pInput->GetUserCmd(0, sequence_number);
	if (!pCmd)
		return;

	CInput::CVerifiedUserCmd *g_pVerifiedCommands = *(CInput::CVerifiedUserCmd**)((DWORD)g_pInput + VERIFIEDCMDOFFSET);
	CInput::CVerifiedUserCmd *pVerified = &g_pVerifiedCommands[sequence_number % MULTIPLAYER_BACKUP];
	if (!pVerified)
		return;	

	CBaseEntity* pLocal = g_pEntList->GetClientEntity(g_pEngine->GetLocalPlayer());
	if (pLocal && pLocal->GetLifeState() == LIFE_ALIVE)
	{
		
		CBaseCombatWeapon* pWeapon = g_pAim.GetActiveWeapon(pLocal);
		if (pWeapon)
		{
			if (g_pCvar.getValue("trigger_active") == 1)
				g_pTrigger.Trigger(pCmd, pLocal);

			static bool enabled = false;
			static bool check = false;

			if (GetAsyncKeyState(0x4E))
			{
				if (!check)
				{
					enabled = !enabled;
					check = true;
				}
			}
			else
			{
				check = false;
			}

			if (enabled)
			{
				g_pCvar.setValue("aimbot_active", 1);
				if (g_pCvar.getValue("aimbot_active") == 1)
					g_pAim.doAim(pCmd, pLocal, pWeapon);
				g_pCvar.setValue("trigger_active", 1);
				if (g_pCvar.getValue("trigger_active") == 1)
					g_pTrigger.Trigger(pCmd, pLocal);
			}
			else
			{
				g_pCvar.setValue("aimbot_active", 0);
				g_pCvar.setValue("trigger_active", 0);
				
			}
			
		}
	}

	g_pTools.NormalizeVector(pCmd->viewangles);
	g_pTools.ClampAngle(pCmd->viewangles);

	pVerified->m_cmd = *pCmd;
	pVerified->m_crc = pCmd->GetChecksum();
}