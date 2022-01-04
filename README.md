Galaxy Busters Elite
-------------
**Name:** Brett Austin

  **Directory Structure:** `GameLab_GalaxyBustersElite/Assets/_Game/Scripts/Mechanics/Enemies/` contain the entirety of the code sample, specifically `/Enemies/EnemyBase.cs`, `/Enemies/EnemyBandit.cs`, `/Enemies/EnemyDrone.cs`, `/Enemies/EnemyMinion.cs`, `/Enemies/EnemyRammer.cs`, `/Enemies/EnemySpearhead.cs`, and `/Enemies/EnemyTank.cs`.

  **Explanation of Code Sample (Specifically my Contributions):** A base enemy class that derives from an EntityBase worked on by another programmer, along with a few child enemy classes that override and add their own unique behaviors. Forms the basis for the enemy types of Galaxy Busters Elite, excluding movement outside of attacks, the Boss enemy, and references to other existing systems in the project.
  - A) Enemy Base that contains:
      - Two enemy states for passive and attacking that are applied to child classes. Enemies start in a passive state that hooks into another programmer's implementation of a node-based movement system that has enemies patrol between specified points. When the player enters an enemy's detection radius, the enemy behavior state switches to attacking and the enemy begins specific attack patterns that vary based on the child class.
      - Getter variable float fields for attack damage, enemy detection radius, and a protected field for an animator component.
      - Protected method for hooking up characteristics of EntityBase, which EnemyBase derives from as well as setting up methods for passive and attacking enemy states. Also an UpdateState method to cover when passive should transition into attacking.
      - Additional public methods for when the enemy dies, takes damage, or enters a trigger.
      - Private method to draw a wire sphere around the specified detection radius only when an enemy object is selected in the editor, meaning this won't be viewable in the playable game.
  - B) Enemy Bandit that contains:
      - Private, exposed float fields for burstShotAmount, attackRate, nextBurstMin and Max, projectileSpeed.
      - Protected override methods for Attacking state functionality, specifically hookups to enemy specific animations as well as if statements that track the current number of burst shots fired as well as an internal cooldown before another burst occurs.
  - C) Enemy Drone that contains:
      - Protected override method for Passive state functionality and specific enemy functionality contained within
      - Protected override method for OnTriggerEnter in order to set enemy-specific animation trigger
  - D) Enemy Minion that contains:
      - Private, exposed floats for attackRateMin and Max, projectileSpeed and Attacking method behavior similar to Enemy Bandit, with shotTime acting as a basic cooldown between shots being fired. Animator triggers are set up within this method to change through various animation states.
  - E) Enemy Rammer that contains:
      - Private, exposed floats for ramSpeed as well as hookups to UnityEvents for OnRamCollide, OnRamAttackEnter and Exit
      - Protected overrides for Passive, Attacking and OnTriggerEnter methods. Mostly contains specific animator triggers as well as specific functionality to get the rammer to move towards the player once the player is in detection range.
  - E) Enemy Spearhead that contains:
    - Private, exposed floats for chargingSpeed, chargeTimerMax, despawnTime and UnityEvents for OnChargeAttackEnter and Exit as well as private bools for isCharging and singleChargeDone. The bools handle internal checks with enemy functionality.
    - Protected overrides for Passive, Attacking and OnTriggerEnter methods and a private method for Charge() functionality. Mostly contains specific animator triggers as well as more complex functionality to get the spearhead to wind up an attack, charge at high speed towards the player's last position in an attempt to hit them as well as functionality to kill itself and another enemy if it hits one on the way to the player.
  - F) Enemy Tank that contains:
    - Private, exposed floats for shot count before tank becomes vulnerable, attackRateMin and Max, vulnerabilityPeriodMax, a BoxCollider reference to turn off the collider when the tank becomes invulnerable, private floats for currentVulnerabilityPeriod and currentShotsCount.
    - Protected override for Attacking method that handles implementation of tracking current amount of shots, cooldown between shots and invulnerability toggling on and off during other functionality.
      
**Date when Code was Written (Excluding Other Programmer's Additions and Forking to Update Readme):**\
First Commit: February 15, 2021\
Final Commit: April 23, 2021
