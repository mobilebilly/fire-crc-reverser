[![Build Status](https://mobilebilly.visualstudio.com/fire-crc-reverser/_apis/build/status%2Fmobilebilly.fire-crc-reverser?branchName=master)](https://mobilebilly.visualstudio.com/fire-crc-reverser/_build/latest?definitionId=1&branchName=master)

# fire-crc-reverser
A utility to find out all the possible inputs to generate a specific crc hex value with brute force
```
Fire.Crc32Reverser
usage: Fire.Crc32Reverser.exe crcHex [validChars]
 crcHex     : crc value in hex (e.g. 7C1FD48A or 0x7C1FD48A)
 validChars : character allowed (e.g. 1234567890ABCabc-[]+=) (default: a-zA-Z0-9)
 ```
