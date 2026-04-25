# Unity Baseball Action MVP

Unity와 C#을 기반으로 제작한 3D 액션 야구게임 MVP 프로토타입입니다.

이 프로젝트는 단순한 타격 미니게임이 아니라, 실제 야구게임의 핵심 흐름인 투구, 타격, 타구 판정, 수비 AI, 송구, 주루, 카운트 시스템을 구조화하는 것을 목표로 합니다.

완성형 상용 야구게임이 아니라, 야구게임 엔진의 핵심 구조를 학습하고 확장하기 위한 시스템 중심 프로젝트입니다.

## Features

- 구종 선택 시스템: 직구, 슬라이더, 커브, 체인지업
- 타이밍 기반 타격 시스템
- 타구 속도 및 궤적 계산
- 페어, 파울, 홈런 판정
- 볼, 스트라이크, 파울, 삼진, 볼넷 카운트 처리
- 야수 자동 추적 AI
- 포구 성공/실패 판정
- 베이스 송구 시스템
- 1루 포스아웃/세이프 판정
- 타자 주자 자동 주루
- 간단한 점수 및 이닝 관리
- 카메라 타겟 전환 구조
- 선수 데이터 및 이적시장 시스템 구조

## Tech Stack

### Client
- Unity
- C#
- Rigidbody-based ball physics
- State machine-based game flow

### Backend Prototype
- Java
- Spring Boot

### Architecture
- Gameplay State Machine
- Rule Judgment System
- Fielding AI
- Player Data System
- Market System Prototype

## Gameplay Flow

```txt
Pitch Selection
→ Pitching
→ Batter Timing
→ Ball In Play
→ Fielding
→ Throwing
→ Runner Decision
→ Play Result
