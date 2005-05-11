
                  =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

                        UNOFFICIAL MADDEN 2005 EDITOR 0.8.0.1

                                   11th May 2005

                                    readme.txt

                                  ----------------

   Contents
   --------

   1. Introduction
   2. Requirements
   3. Features
   4. Fixed Issues
   5. Known Issues
   6. Developer Information
   7. Future Plans
   8. Licence

1. INTRODUCTION
===============
Since the release of TDBAccess from Suchy63 editors have begun cropping up for 
Madden 2005. This is just another one to choose from. This project is open 
source and anyone is actively encouraged to develop it.

2. REQUIREMENTS
===============
The main requirement for this program is .NET Framework Runtime v2.0 Beta 2.
You need to install this in order to run the program. Also NOTE, if you have a 
previous .NET Framework Runtime v2.0 Beta installed, uninstall it first.

This can be downloaded from 
http://www.microsoft.com/downloads/details.aspx?FamilyID=7ABD8C8F-287E-4C7E-9A4A-A4ECFF40FC8E&displaylang=en

The Actual package name is .NET Framework Version 2.0 Redistributable Package Beta 2 (x86).

3. FEATURES
===========

* (NEW) Thanks to zentrarium we now have the ability to set team Captains!!! Available in the
        Franchise menu option when loading a franchise file.

* (NEw) Support for Madden 2004 files. (Only tested with Roster files at the moment, due to me
        not having any franchise files)

* (NEW) Global Attribute Editing (version 1)
        Added a Tool to set/increment/decrement player attributes.
        You can filter by the usual team/position/draft class options

* (NEW) Coach editing is all corrected including all Priorities and balanced/aggressive/conservative
        etc..
        i.e QB's can have a priority set to scrambing/balanced/pocket

* Depth Chart Editing. 
        PLEASE BE AWARE!!!! This lets you set any player to any position, However, i have 
        experimented and it seems the game doesnt allow you to add certain players to 
        positions. For example, I couldnt get the game to allow Lineman to play QB.
        
        In fact I couldnt even put a WR at CB. That sucks!!

* Coach searching.

* Better editing of Salary. Changes should be effecting Team Salary too now

* Editing of RFA Minimum Tenders and Salary Cap in Franchise files

* Editing of some Coach attributes

* Adding/removing of injuries with full description and bug fixes

* Delete function

* Added Draft Class Filter for easy editing of draft classes

* Supports loading franchise files

* Added support to estimate OVR rating by hitting calculate
        (From files by ReMiNiScE @ football-freaks.com)

* Nearly all player attributes (bar 5) are now editable

* Fast Search function to find specific players

* Filter functions to narrow browsing down making it easy to move through
  players on teams/positions

4. FIXED ISSUES
===============

* Fixed issue with saving roster files in version 0.8.0.0

5. KNOWN ISSUES
===============

* Tabbing between settings doesnt work correctly

* Probably more issues that are not listed here

  NOTE: Please, if you get a crash, email the details of the crash (Found in 
        the details section of the crash box) to bugs@tributech.com.au.
  
6. DEVELOPER INFORMATION
========================
This product is being developed with Visual C# Express Beta 2.

The source is available from a subversion server. You will need a subversion
client in order to retrieve the latest source code.

I recommend
http://tortoisesvn.tigris.org  

You can retrieve the latest source at
http://gommo.homelinux.net/svn/repos/maddeneditor/trunk

(Note: If the checkout is slow be patient, I'm only on a 1500/256 ADSL connection 
 and I'm in Australia, so its probably coming from the other side of the world)

You will need to contact colin@tributech.com.au to get access rights to check
code back in, but anyone is free to get the source code.

You can also browse the repository at the above address too.

This source has only been in development for a few days now so I've been very 
busy just getting things to work. As a result, lots of comments have been
missed :( . Sorry, I will endevour to clean up the code a bit when I get the
full player attribute editing going.

Also, I'll probably start up a bugzilla project for reporting of bugs etc.. if
theres enough reason too.

The setup file is generated with NSIS (nsis.sourceforge.net)

7. FUTURE PLANS
===============

* Remove Cap Penalty ability

* Complete all attribute editing of players and coaches

* Add Team editing

* Create Player function 

* Add player picture from DAT files to Player screen

* Provide a better crash exception handling box with email functionality

* More...

8. LICENCE
==========
This program is developed under the LGPL. See licence.txt for full licence.


