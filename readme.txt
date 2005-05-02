
                  =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

                        UNOFFICIAL MADDEN 2005 EDITOR 0.5.1

                                  2nd May 2005

                                   readme.txt

                                ----------------

   Contents
   --------

   1. Introduction
   2. Requirements
   3. Features
   4. Known Issues
   5. Developer Information
   6. Future Plans
   7. Licence

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

* Currently supports editing of most player attributes from a Madden 2005 
  roster file. Attributes that are not supported yet are either grayed out
  or are just not existent in the editor

* Search function to find specific players

* Filter functions to narrow browsing down making it easy to move through
  players on teams/positions

4. KNOWN ISSUES
===============

* Currently doesnt load up franchise files. This is deliberate. This program 
  has only began being written on the 29th of April 2005 so is still very
  young

* Tabbing between settings doesnt work correctly

* Probably more issues that are not listed here

  NOTE: Please, if you get a crash, email the details of the crash (Found in 
        the details section of the crash box) to bugs@tributech.com.au.
  
5. DEVELOPER INFORMATION
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

6. FUTURE PLANS
===============

* Complete all attribute editing of players

* Add player picture from DAT files to Player screen

* Add Team editing

* Add support for franchise editing

* Provide a better crash exception handling box with email functionality

* More...

7. LICENCE
==========
This program is developed under the LGPL. See licence.txt for full licence.


