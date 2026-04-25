# Scene Setup Guide

## 필수 오브젝트

### Ball
- Sphere 생성
- Rigidbody 추가
- BallController 연결
- Use Gravity 체크

### Pitcher
- Empty 또는 Capsule
- PitcherController 연결
- releasePoint 생성 후 연결

### Batter
- Empty 또는 Capsule
- BatterController 연결
- idealContactPoint 생성 후 연결

### StrikeZone
- Empty 오브젝트
- 홈플레이트 위쪽에 배치

### Fielders
9개 Capsule 생성:
- Pitcher
- Catcher
- FirstBase
- SecondBase
- ThirdBase
- ShortStop
- LeftField
- CenterField
- RightField

각각 FielderController 연결

### Bases
4개 Empty 생성:
- Home
- First
- Second
- Third

각각 BaseController 연결

## 추천 좌표

```txt
Home:       (0, 0, 0)
Pitcher:    (0, 0, 18)
First:      (12, 0, 12)
Second:     (0, 0, 24)
Third:      (-12, 0, 12)
LeftField:  (-22, 0, 42)
Center:     (0, 0, 52)
RightField: (22, 0, 42)
```

## 카메라

처음에는 Ball을 따라가게 두면 됩니다.
나중에는 상태에 따라 Pitcher/Batter/Ball/Fielder로 타겟을 바꾸면 됩니다.
