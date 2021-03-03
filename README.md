# Disclaimer
**THIS IS NOT THE OFFICIAL RBOXLO REPOSITORY!!! The real one is [here](https://github.com/lightbulblighter/Rboxlo). All issues, PRs, etc. will be ignored. This is also not an Rboxlo sequel, I just published this on GitHub for commit signing + interaction with some tools (e.g. FOSSA).**
<br>
<br>

<p align="center"><img src="https://github.com/lightbulblighter/Rboxlo2/raw/main/Branding/Logos/Primary/Big.png" width="60%"><br><i>Project Eclipse 🌙</i></p>
<hr>

Rboxlo is a **free, open-source** Roblox private server application that can be used to relive childhood memories and create new ones. Using Rboxlo, you can set-up your own Roblox private server in minutes with the ability to play Roblox clients dating all the way back to 2007, and as modern as 2017. Rboxlo was created to truly decentralize Roblox, and to eventually eclipse the proprietary closed-source Roblox revivals that exist today.

## Features

<img src="https://github.com/lightbulblighter/Rboxlo2/raw/main/Branding/Artwork/SwordNoob.png" align="right" width="20%">

**Fully open-source and decentralized**

There is no official "Rboxlo hub" or any network where Rboxlo instances connect to and meet up. What you do on your Rboxlo instance remains private forever on your instance. The entirety of Rboxlo's source code is available here, and is released into the public domain, making it so that nobody can claim individual copyright over any part of Rboxlo.

**Multiple client version support**

Rboxlo supports Roblox clients as old as 2007 to Roblox clients as modern as 2017. No matter what Roblox game executable you have, it will work on Rboxlo. Rboxlo also implements all Roblox API endpoints, past or present, to ensure 100% stability.

**Easy to set-up**

Setting up Rboxlo is a process that takes only a matter of minutes. You do not need to be a proficient sysadmin to figure out how to get the ball rolling.

**Modern-day codebase, secure**

Rboxlo does not use archaic technology, and is built using modern-day containerization. Rboxlo also tries to use only the best security practices wherever possible.

**Customizability**

Make Rboxlo yours by customizing every single aspect of the platform. Whether it be the sites theme, the logos, the artwork, or even the color of the login button, everything is customizable from a single web panel.

**Cross-platform**

Rboxlo officially supports Linux and Windows out of the box at the server and client level. Though Roblox does not support Linux itself, Rboxlo attempts to use Wine to launch Roblox.

## Deployment

**Tech stack:**

- **Node.js** powers the server and everything surrounding it (REST API, matchmaker, etc.)
- **MySQL** powers the server's database, where all permanent data is stored
- **Docker** containerizes the server and runs it all in a secure fashion
- **.NET** is what the client applications are built on for smooth execution and cross-platform support

**Requirements:**

- **Docker** 20.10.0+
- **Visual Studio** 2019+

The requirements are very short and sweet because Docker is used to run the entire server, and Visual Studio is used to build the client applications. Detailed instructions on how to deploy Rboxlo are available in [SETUP.MD](https://github.com/lightbulblighter/Rboxlo2/blob/main/SETUP.md).

## Contributing

Rboxlo is public domain software licensed under the **Unlicense**.

We ask that all potential contributors take a look at the [code of conduct](https://github.com/lightbulblighter/Rboxlo2/blob/main/CONTRIBUTING.md) before contributing in order to ensure healthy discourse.

If you have a contribution to make, [please "unlicense" it.](http://unlicense.org/) The reason behind this requirement is because of some vague copyright laws in the United States allowing for the usage of loopholes that could potentially deteroriate Rboxlo's public domain status. **This only applies to pull requests.**

If you find any problem(s) in Rboxlo, feel free to submit an issue. This includes stuff like vulnerabilities, or even the most trivial issues (such as typoes.)

If you would like to suggest a feature or change, submit it as an issue as well; it will be given the appropriate tag once we have seen it.

If you know how to fix an issue, feel free to make a pull request for the issue.

## License

This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or distribute this software, either in source code form or as a compiled binary, for any purpose, commercial or non-commercial, and by any means.

In jurisdictions that recognize copyright laws, the author or authors of this software dedicate any and all copyright interest in the software to the public domain. We make this dedication for the benefit of the public at large and to the detriment of our heirs and successors. We intend this dedication to be an overt act of relinquishment in perpetuity of all present and future rights to this software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org/>