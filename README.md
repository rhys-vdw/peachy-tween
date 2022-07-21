# PeachyTween :peach:

[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

A tweening for Unity designed to be simple to use and extend.

## Background

Created as an alternative to [DOTween](https://github.com/Demigiant/dotween/), which has a proprietary license with some limitations. PeachyTween intends to create a similarly powerful tweening library that can be installed by UPM and allows public forks and distribution.

PeachyTween does not aim to be API compatible with DOTween, not to fully mirror its functionality.

:warning: **PeachyTween is still in early development, and is untested**

## Install

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

## Contributing

Contributions welcome. If you wish to make a pull request, please open an issue first.

See [development project](https://github.com/rhys-vdw/peachy-tween/projects/1).

## License

[MIT © Rhys van der Waerden](LICENSE)