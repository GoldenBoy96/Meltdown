# SmoothCamera Component

A Cinemachine-based 2D camera that follows the player with dynamic zoom, damping, and map bounds.

## Prerequisites

- Scene must have a **CineCam** GameObject with:
  - `CinemachineVirtualCamera`
  - `SmoothCamera` (this script)
- Main Camera needs a `CinemachineBrain` component.

## Inspector Setup

- **Map Collider**: assign a `PolygonCollider2D` that defines your map bounds.

## Public API

- `DisconnectCamera()`  
  Stops following the player.
- `ConnectCamera()`  
  Re-links to the current player (use after respawn).
- `SetMapBound(PolygonCollider2D mapBound)`  
  Swap the map bounds at runtime.

## Customization

These private constants control camera behavior. Tweak and recompile to adjust feel:

- `BaseOrthoSize` (default 10)  
  The base orthographic size. Larger = more world visible.
- `BaseZPos` (default -10)  
  Camera’s Z position. Should stay behind the action.
- `BaseZoomSpeed` (default 2.2)  
  How fast the camera zooms in/out when the player moves.
- `BaseZoomInSize` (default 0.8)  
  Zoom reduction factor at max player speed. Higher = more zoom-in.
- `DampingSpeed` (default 0.5)  
  How quickly the camera’s follow lag adapts to player input.
- `BonusDamping` (default 0.1)  
  Minimum follow lag. Higher = more smoothing even at zero input.

## How to Use

1. Place the **CineCam** prefab in your scene.
2. Drag your map’s `PolygonCollider2D` into **Map Collider**.
3. Ensure `GameViewController.Player` is assigned before play.
4. (Optional) On player respawn:
   ```csharp
   smoothCamera.DisconnectCamera();
   // …respawn…
   smoothCamera.ConnectCamera();
   ```
