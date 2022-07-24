# PeachyTween :peach:

[![openupm](https://img.shields.io/npm/v/computer.rhys.peachy-tween?label=openupm&registry_uri=https://package.openupm.com&style=flat-square)](https://openupm.com/packages/computer.rhys.peachy-tween/)
[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

A tweening library for Unity designed to be simple to use and extend.

## Background

Created as an alternative to [DOTween](https://github.com/Demigiant/dotween/), which has a proprietary license with some limitations. PeachyTween intends to create a similarly powerful tweening library that can be installed by UPM and allows public forks and distribution.

PeachyTween does not aim to be API compatible with DOTween, nor to fully mirror its functionality.

:warning: **PeachyTween is still in early development, and is untested**

## Install

### Installing from OpenUPM

[OpenUPM package](https://openupm.com/packages/computer.rhys.peachy-tween/).

Package can be installed from [OpenUPM](https://openupm.com/) using [OpenUPM CLI](https://openupm.com/docs/getting-started.html);

```console
$ openupm add computer.rhys.peachy-tween
```

### Installing from GitHub

[GitHub repository](https://github.com/rhys-vdw/peachy-tween).

To add to you Unity project, first add the internal dependency [ecslite](https://github.com/LeoECSCommunity/ecslite).

In Unity package manager, select "Add package from git URL..." and add the following two URLs:

1. `https://github.com/LeoECSCommunity/ecslite.git#1.0.0`
2. `https://github.com/rhys-vdw/peachy-tween.git`

Or add the following two lines to `dependencies` in your `manifest.json`:

```json
"dependencies": {
  "com.leoecscommunity.ecslite": "https://github.com/LeoECSCommunity/ecslite.git#1.0.0",
  "computer.rhys.peachy-tween": "https://github.com/rhys-vdw/peachy-tween.git",
```

_Installation will streamlined before full release._

## Usage

```cs
using UnityEngine;
using PeachyTween;

public class PeachyExample : MonoBehaviour
{
  void Start()
  {
    Peachy.Tween(
      from: 0f,
      to: 5f,
      duration: 4f,
      onChange: v => Debug.Log($"value = {v}")
    );

    transform
      .TweenLocalRotation(Quaternion.Euler(90, 90, 90), 2f)
      .Slerp()
      .Ease(Ease.BounceInOut)
      .Loop(4)
      .OnLoop(() => Debug.Log("Loop complete"))
      .OnKill(() => Debug.Log("Four loops complete"));
  }
}
```

## API

[API Documentation](https://peachy-tween.rhys.computer/api/PeachyTween.html).

## Contributing

Contributions welcome. If you wish to make a pull request, please open an issue first.

See the [GitHub project](https://github.com/rhys-vdw/peachy-tween/projects/1) for the development roadmap.

### Environment setup

To work on PeachyTween:

1. Clone the GitHub repo.
2. Create a new Unity project.
3. In the package manager window, select "Add package from disk..." and select your local PeachyTween folder.
4. Enable tests in the editor ([see below](#enabling-tests-in-the-editor)).

### Enabling tests in the editor

Contributions should have test coverage. To run the test suite, edit your project's `manifest.json` to include the following entry:

```json
  "testables": [
    "computer.rhys.peachy-tween"
  ]
```

## License

[MIT © Rhys van der Waerden](https://github.com/rhys-vdw/peachy-tween/blob/main/LICENSE)
