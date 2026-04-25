# Baseball Full Action MVP

Unity + C# 기반 액션 야구게임 MVP 템플릿입니다.

이번 버전은 단순히 공을 치고 야수가 따라가는 수준을 넘어서,
야구게임처럼 보이기 시작하는 핵심 판정 구조를 포함합니다.

## 들어간 기능

```txt
투구 선택
타격 타이밍
타격존 판정
타구 속도/각도 계산
파울/안타/장타/홈런/아웃 판정
스트라이크/볼/삼진/볼넷 카운트
야수 자동 추적
포구 성공/실패
송구
1루 포스아웃 판정
주자 자동 진루
간단한 HUD 연결용 클래스
카메라 타겟 전환 구조
이적시장/선수 능력치 연결 가능 구조
```

## 조작

```txt
P: 투구
Space: 스윙
1: 직구
2: 슬라이더
3: 커브
4: 체인지업
```

## Unity 세팅

1. Unity Hub에서 3D 프로젝트 생성
2. `unity-client/Assets/Scripts`를 Unity 프로젝트의 `Assets/Scripts`로 복사
3. Ball 오브젝트에 Rigidbody 추가
4. GameObject 구성:
   - ActionGameManager
   - Pitcher
   - Batter
   - Ball
   - DefenseManager
   - RunnerManager
   - ScoreManager
   - HUDController
   - CameraRig
5. Inspector에서 public 필드를 연결

## 중요한 판단

이건 완성 게임이 아닙니다.
하지만 아래 루프는 실제 게임 구조로 확장 가능합니다.

```txt
구종 선택 → 투구 → 타격 입력 → 판정 → 타구 → 수비 → 송구 → 아웃/세이프 → 카운트/이닝 업데이트
```

이 정도까지 만들면 단순 과제나 포트폴리오에서는 “진짜 게임 시스템을 설계했다”라고 말할 수 있습니다.
