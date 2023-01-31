## 2023-01-31
- 게임 시스템 변경에 따른 프로젝트 변환
- 변경점 : Localization 기능 추가, InputAction 제거, Firebase 제거, GPGS 제거

## 2023-01-27
- 보스전 진행 로직 구현
- 스테이지 진행 흐름 구축

## 2023-01-25
- 몬스터 소환 시스템 구축 (소환 로직 구성)
- 공격시 타격 판정이 안되는 현상 수정
- 몬스터 등장 이펙트 추가

## 2023-01-18
- 캐릭터 FSM 보완 (몬스터 타겟팅, 자동 공격)
- 스테이지 진행 시스템 제작

## 2023-01-16
- 카메라 진동 기능 추가
- 몬스터 FSM 보완 (리젠, 추적, 공격, 피격, 죽음)
- 캐릭터 공격 애니메이션 연타시 정지 현상 수정
- 테스트를 위한 치트 메뉴 제작

## 2023-01-12
- 몬스터 캐릭터 따라가기 및 공격 모션까지 처리

## 2023-01-11
- URP 적용을 위한 UnityEditorVersion 변경 (2021.3.3f1 LTS)
- CineMachine 카메라를 이용한 캐릭터 추적 기능 추가
- 캐릭터 애니메이션 제작 - 이동

## 2023-01-10
- Editor상 Scene 바로가기 기능 추가
- cold start 기능 구현 (이전 씬에서 세팅할 정보가 없는 상태로 개별 씬에서 게임 시작 가능)

## 2023-01-09
- FSM 구조 추가 완료 및 FSM 어셈블리 분리

## 2023-01-05
- 캐릭터 소환 로직 적용 및 카메라 추적 적용
- 오브젝트의 동작 정의를 위한 FSM 추가 작업 진행 중

## 2023-01-04
- ScriptableObject를 이용한 Scene 참조 방식으로 아키텍쳐 변경
	- 장점 : 싱글톤 클래스 최소화 (씬간 OOP 원칙 준수), Addressable을 통한 씬 패치 가능
- Scene 용도에 따라 프로젝트 상주씬, 메인화면 씬, 게임화면 씬, 스테이지 맵 씬으로 분리
- 화면 전환에 따른 로딩 화면 컨트롤러 클래스, 페이드 인/아웃 컨트롤러 클래스 추가
- Input System을 다양한 Device에 대응 가능한 Input Action 시스템으로 변경
	- 장점 : 같은 입력키를 현재 활성화된 화면에 따라 다른 동작을 하도록 미리 지정 가능

## 2023-01-03
- 몬스터 근접시 자동 공격 추가
- 캐릭터 주변 몬스터 인지 기능 추가

## 2023-01-02
* 캐릭터 입력 처리를 위한 Input System Asset 설치
* 스테이지 시스템 제작
* 몬스터 리젠 시스템 제작
* 배경 맵 애니메이션 효과 테스트

## 2022-12-30
* 빌드 버전 자동 관리 시스템 추가
* CI 씬 추가
* 캐릭터 추적 카메라 추가

## 2022-12-29
* Unity Project 버전 변경 (2020.3.24f1 LTS - SDK 호환성 관련 문제)
* Error Log 확인용 Gpm 설치
* GPGS 0.11.01 -> 10.14 변경 (코드 변경으로 인한 다운그레이드)
* 구글 - Firebase 연동 로그인 관련 세팅
* Font 추가
* 로그인 화면 구성
* 애니메이션 처리를 위한 DoTween 설치

## 2022-12-28
* ~2D URP Project 생성 (2021.3.16f1 LTS)~
* ~GPGS 설치 (0.11.01)~
* Firebase Auth 설치 (10.3.0)

---

## Include Asset & SDK & Plugin & Package

+ Engine Setting
  + ~Unity 2020.3.24f1 LTS~ (2023-01-11 제거)
  + Unity 2021.3.3f1 LTS


+ Server Side SDK
	+ Firebase Unity SDK (10.3.0)
		+ FirebaseAuth : 파이어베이스 인증

+ Plugin
	+ Google Play Game Service (GPGS) : 구글 플레이 게임 서비스 연동
		+ Play Games Plugin For Unity (10.14)

+ Asset
	+ Game Package Manager for Unity (GPM) : 유니티 유틸리티 도구 모음
	+ DoTween (HOTween v2) : 유니티 애니메이팅 기능

+ Package
	+ Addressables (1.19.19)
	+ Input System (1.3.0)
	+ Cinemachine (2.8.9)
	+ Android Logcat (1.3.2)