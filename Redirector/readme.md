redirector server for GW2

to use, apply the SSL patch and add winter15.gosredirector.ea.com to your hosts file. after building, copy the certs folder to be with the server binary

requires port 42230 to be open, listens on localhost by default

it redirects to 127.0.0.1:10041 by default, this can be changed in the xml in utils.cpp