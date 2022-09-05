# squirrel-uucp

Squirrel UUCP utilities enable Windows users to easily utilize UUCP for purposes of data transfer.

Right now that consists of the Squirrel UUCP Caller.
 
# Squirrel UUCP Caller

Squirrel UUCP Caller is a GUI frontend--developed using Visual Basic .NET/Microsoft Visual Studio 2022--that lets a casual user batch up files, setup and select a UUCP system, and make calls.

UUCP caller integrates with a Cygwin environment to do the actual work.  

## Cygwin Integration

- You can use the supplied Cygwin environment, which includes a necessary bridge script, enough Cygwin packages for basic shell use and SSH, and precompiled Taylor UUCP binaries.

- You can point UUCP Caller to an existing Cygwin environment if you have one.  You will need to compile and copy over the Taylor UUCP binaries yourself, as well as the bridge script.  I found Taylor UUCP easy to build.

- Integration with Windows Subsystem for Linux has not been explored yet.

# UUCP Invite Files

UUCP implements the UUCP Invite File specification.

Invite files are text files that contain all the parameters of a UUCP system - all a user has to do is point UUCP Caller to the invite file and it will be processed, configured, and added to the list of systems available to call.

If you operate a UUCP capable of receiving calls, a bash script is available that you can use to generate invite files.

* At this time only SSH-based transports into a UUCP receiver are supported by UUCP caller.  More will probably be added in the future.

# Installation

# Usage

# Why?

In 2022 I became fascinated with UUCP - it was the "Internet before the Internet" - a peer-to-peer network of computers that would call each other periodically and enable the transfer of files and messages.

On Linux, you can still install and use UUCP, and it works great.  And you'll be setting up config files and learning all about the intricacies of UUCP before you make your first call, unless someone guides you.  

Now, let's say you took the time, learned all about how UUCP works, and have the ability to receive UUCP calls.  Now you want to get others in on the deal and build yourself a nice UUCP network.  But it's 2022, not 1982 - and likely anyone you reach out will not know what UUCP is, nor care to invest the time to set it up through manual editing of configuration files.  Many computer users today are mostly living a world of super-mobile smartphone/tablet platforms; with interfaces that are simple and visual.

Thus I wanted something to get new users up to speed quickly, and also work on Windows.  

Only thing I really found "out there" was [UUPC/Extended](https://www.uupc.net/) - an ancient suite of DOS executables and what I think are Windows 3.1 style graphical elements bolted on top of of them - firmly of the Windows 3.1/95 16-bit era.  You still need to be waist-deep in UUCP configuration file and working knowledge to use this.  

I couldn't really find anything super "easy".   And that's OK--UUCP's heyday was the late 70's/early 80's, and it was on the wane in the early 90's - not too long before Windows 95 and AOL took the world by storm, and newfangled things like the WWW, HTTP, FTP, and SMTP/POP3/IMAP wrapped up in pretty graphical clients became what most people were using.

UUCP is fascinating.  If you want to learn more about UUCP, consult the following resources:

* [UUCP - the Wikipedia Article](https://en.wikipedia.org/wiki/UUCP)
* [My "UUCP in 2022" .PDF](https://github.com/lawrencecandilas/craziness/tree/main/UUCP%20in%202022)

# Things I hope to have the time to do in the future

- Figure out a way to get Cygwin terminal output to funnel through controls in realtime.  

	- Right now, when you make calls, a terminal window pops up - showing `uucico`'s output and enabling tracking of the call.

	- The original intent was for those lines to go in to the logging window, but everything I tried would not move the data to the window until the entire command was completed - and calls can take several minutes or even longer if the data amount is large.

- The whole nexus of the objects representing UUCP things and how it talks to the UI could be much better.  I was mostly figuring things out as I was going along with this.

- Add more transport methods - including pure serial, dial-up, and `bondcat` if it will compile/work under Cygwin.

- Document how to add transport methods and try to make them as easy as possible to add.

- Develop Squirrel UUCP Receiver.


