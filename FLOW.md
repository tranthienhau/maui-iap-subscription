# Screenshot / demo capture

How the committed screenshots and `demo.gif` were produced. Not needed to run the app.

## Prerequisites

- .NET 9 SDK + MAUI workload (`dotnet workload install maui-ios maui-android`)
- Xcode + an iOS simulator (these shots used **iPhone 17 Pro**)
- `ffmpeg` (GIF + frame extraction)

## Build & install

```bash
export DOTNET_ROOT="$HOME/.dotnet"; export PATH="$DOTNET_ROOT:$PATH"

# Boot a simulator
xcrun simctl boot "iPhone 17 Pro"
open -a Simulator

# Build for the simulator (no code signing needed)
dotnet build -f net9.0-ios -p:RuntimeIdentifier=iossimulator-arm64 -p:CodesignKey=""

APP=bin/Debug/net9.0-ios/iossimulator-arm64/SubscriptionPro.app
xcrun simctl install booted "$APP"
```

## Demo entry modes

The app reads a `DEMO_MODE` env var (passed through the simulator with the
`SIMCTL_CHILD_` prefix) so each screen is reachable deterministically. See
`Bootstrap.cs`.

| Mode | Effect |
| --- | --- |
| `paywall` (default) | Normal launch on the paywall |
| `success` | Seeds an active annual subscription, jumps to the success screen |
| `manage` | Seeds an active subscription, jumps to manage |
| `error` | Forces the purchase to fail so the error dialog shows |
| `demo` | Autoplays paywall -> select -> purchase -> success -> manage |

## Capture stills

```bash
BID=com.subscriptionpro.premium
shot(){ xcrun simctl terminate booted "$BID"; \
  SIMCTL_CHILD_DEMO_MODE="$1" xcrun simctl launch booted "$BID"; \
  sleep "$3"; xcrun simctl io booted screenshot "screenshots/$2"; }

shot paywall 01-paywall.png 6
shot success 03-success.png 6
shot manage  04-manage.png 6
shot error   05-error.png  5
```

## Record the demo GIF

```bash
BID=com.subscriptionpro.premium
xcrun simctl io booted recordVideo --codec h264 /tmp/demo.mov &
REC=$!
SIMCTL_CHILD_DEMO_MODE=demo xcrun simctl launch booted "$BID"
sleep 13; kill -INT $REC

# Palette-optimized GIF + a processing-state still
ffmpeg -y -ss 1.5 -t 11 -i /tmp/demo.mov -vf "fps=12,scale=280:-1:flags=lanczos,palettegen=stats_mode=diff" /tmp/pal.png
ffmpeg -y -ss 1.5 -t 11 -i /tmp/demo.mov -i /tmp/pal.png \
  -lavfi "fps=12,scale=280:-1:flags=lanczos[x];[x][1:v]paletteuse=dither=bayer:bayer_scale=3" screenshots/demo.gif
ffmpeg -y -ss 5 -i /tmp/demo.mov -frames:v 1 screenshots/02-processing.png
```
