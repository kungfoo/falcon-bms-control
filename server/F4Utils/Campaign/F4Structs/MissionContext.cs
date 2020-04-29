using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4Utils.Campaign.F4Structs
{
    // Mission request contexts (Why this mission is being requested)
    public enum MissionContext 
    {	
        noContext,						// We don't really know why
		enemyUnitAdvanceBridge,			// An enemy unit is advancing over a bridge
		enemyUnitMoveBridge,			// An enemy unit is moving over a bridge
		enemyUnitAdvance,				// An enemy unit is advancing
		enemyUnitMove,					// An enemy unit is moving
		enemyUnitAttacking,				// An enemy unit is attacking our forces
		enemyUnitDefending,				// An enemy unit is defending against our forces
		enemyForcesPresent,				// Enemy forces are suspected to be here
		attackEnemyUnit,				// FAC CALL: Attack specific enemy unit (8)
		emptyUnit1,						// TBD (9)
		emptyUnit2,						// TBD (10)
		friendlyUnitAirborneMovement,	// a friendly unit needs airborne transportation
		emptyUnit5,						// TBD (12)
		emptyUnit6,						// TBD (13)
		enemyStrikesExpected,			// Enemy air strikes expected in this area
		enemyAircraftPresent,			// Enemy aircraft are expected (generic) (15)
		enemyGroundForcesPresent,		// Enemy ground forces are expected (JSTAR trigger)
		enemyRadarPresent,				// Enemy radar operating (ECM trigger)
		enemySupportAircraftPresent,	// Enemy support aircraft operating
		enemyCASAircraftPresent,		// Enemy ground attack aircraft are present
		interceptEnemyAircraft,			// AWACS CALL: Intercept specific enemy aircraft (20)
		emptyEnemyAir1,					// TBD (21)
		hostileAircraftPresent,			// Hostile aircraft operating in an area
		emptyHostileAir1,				// TBD (23)
		friendlyRescueExpected,			// Friendly SAR craft will be operating in the area
		friendlyCASExpected,			// Friendly CAS aircraft will be entering the area (25)
		friendlyAssetsExpected,			// Generic 'Friendly assets' will be operating here
		friendlyAssetsRefueling,		// Friendly aircraft will be refueling in the area (TANKER trigger)
		emptyFriendly1,					// TBD (28)
		emptyFriendly2,					// TBD (29)
		enemySupplyInterdictionBridge,	// Enemy supplies are being transported through here (30)
		enemySupplyInterdictionPort,	// Enemy supplies are being transported through here
		enemySupplyInterdictionDepot,	// Enemy supplies are being stored here
		enemySupplyInterdictionZone,	// Enemy supplies are being moved through here
		emptySupply1,					// TBD (34)
		enemyProductionSource,			// This is producing enemy war materials
		enemyFuelSource,				// This is producing or storing fuel.
		enemyEnergySource,				// This is a source of enemy electrical power
		enemyCommand,					// This is being used as an enemy CCC module
		enemyAirDefense,				// Enemy air defenses are blocking friendly missions
		enemyAirPowerAirbase,			// This is being used to promote enemy air power (40)
		enemyAirPowerRadar,				// This is being used to promote enemy air power
		emptyObj1,						// TBD (42)
		emptyObj2,						// TBD (43)
		friendlyAWACSNeeded,			// We need an awacs (AWACS trigger)
		friendlySuppliesIncomingAir,	// We've got friendly supplies coming in by air
		friendlySuppliesIncomingNaval,	// Same by naval
		friendlySuppliesIncomingGround,	// Same by ground
		friendlySuppliesIncomingRail,	// Same by rail
		targetReconNeeded,				// Need recon of this objective
		emptyx,							// TBD (50)
		emptyy,							// TBD (51)
		AirActionPrepAD,				// OCA vs Air Defenses part of any air action
		AirActionPrepAB,				// OCA vs Air Bases/Radar part of non-OCA action
		AirActionPrepAir,				// Sweep/Escort part of a any action
		AirActionDCA,					// Part of a DCA action
		AirActionOCA,					// Part of an OCA action
		AirActionInterdiction,			// Part of an interdiction action
		AirActionAttrition,				// Part of an attrition action
		AirActionCAS,					// Part of a CAS action (59)
		emptyAction1,					// TBD (60)
		enemyNavalForceActive,			// Ships are operating here
		enemyNavalForceStatic,			// Ships in port
		enemyNavalForceUnloading,      // Transport ships unloading in port
		InterceptWithAlertOnly,         
		otherContext 
    }
}
