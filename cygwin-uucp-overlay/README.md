These files contain two things:

1. UUCP binaries compiled under Cygwin.  Original source is on [Ian Taylor's website](https://www.airs.com/ian/software.html).  A Github mirror of the source is available [here](https://github.com/quinot/taylor-uucp) - a mirror maintained by [quinot](https://github.com/quinot).

2. The `uucico-bridge` script that UUCP Squirrel Caller uses to invoke UUCP binaries also lives here (in /usr/local/bin).

3. Binaries related to enabling future transport abstraction scenarios.  Currently included is:

 * Cygwin SSH packages to support SSHTransport.

 * Windows Tor binaries from [the Tor project's "Windows Expert Bundle"](https://www.torproject.org/download/tor/).  I'm not too interested in it's anonymity or privacy features, but Tor hidden services make it easy for a node to receive a call without having to worry about port forwarding or firewalls.  If experimentation is positive, this would be included in a possible future Squirrel UUCP Receiver.

The idea is that you can take this entire directory structure and overlay it over an existing Cygwin installation, and then your existing Cygwin installation is ready to use with UUCP Squirrel Caller.
