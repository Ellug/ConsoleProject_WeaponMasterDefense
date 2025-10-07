# WeaponMasterDefense

- 이 프로젝트는 **C#**, **.NET Framework 4.7.2** 환경에서 제작되었습니다.
- Windows11에 새로 도입된 텝 방식의 터미널에서는 정상적으로 출력이 되지 않으니 반드시 Windows 터미널이 아닌 Windows 콘솔 호스트로 환경에서 실행하시길 바랍니다.
- 플레이어를 조작해 방어 라인으로 향해 다가오는 적들을 처리하는 간단한 디펜스 게임입니다.
- 범위 안에 들어온 몬스터에게 플레이어가 자동으로 탄환을 발사합니다.
- 적을 처리할 때마다 SCORE가 올라가며, 일정 SCORE 마다 레벨업을 합니다.
- 레벨업 시, 랜덤으로 주어지는 옵션 중 하나를 선택해 플레이어를 강화하거나 방어벽을 보수할 수 있습니다.
- 배열을 통해 렌더링 할 데이터를 비교하여 출력하는 2중 버퍼 렌더링을 흉내내 보았습니다.
- Firebase를 통해 랭킹 서버를 연결했습니다.
---
- This project is developed in **C#** with **.NET Framework 4.7.2**.
- Please note: the game may not render correctly on **Windows 11 tab-style Terminal**. Make sure to run it using the **Windows Console Host**, not Windows Terminal.
- The game is a simple **defense-style console game** where the player must intercept and eliminate enemies approaching the defense line.
- The player automatically fires projectiles at enemies within range.
- Each defeated enemy increases the **SCORE**, and upon reaching certain thresholds, the player **levels up**.
- On level up, you can choose one of the randomly provided options to either **enhance your character** or **repair the defense wall**.
- Implemented a double-buffered rendering system that compares two frame arrays to optimize console output.
- Integrated Firebase Firestore to handle the online ranking system.
---
## 기본 조작법 Basic Controls
- 이동 : ← ↑ ↓ → (방향키)
- 스킬 : Q W E R
  
- Movement: ← ↑ ↓ → (Arrow Keys)
- Skills: Q, W, E, R
---
# 프로젝트 구성
## Enemy Folder
적 몬스터에 대한 소스 파일들
- Monster.cs : 몬스터들에 대한 추상클래스. 공통 속성 및 동작 정의. 개별 구현은 Monsters 폴더 내부.
- MonsterFactory.cs : 몬스터 생성을 전담하는 팩토리 패턴 클래스. 레벨에 따라 등장 몬스터를 관리하고 보스 생성 로직 포함.
- MonsterSpawner.cs : 몬스터 스폰 주기와 개체 수를 제어하며, 생성 / 업데이트 / 제거를 관리하는 메인 컨트롤러 역할.
## Input Folder
- InputSystem.cs : 게임 플레이 중 인풋 시스템을 담당. 유니티의 키 다운, 키 업을 이용한 부드러운 입력 기법을 사용할 수 있도록 GetAsyncKeyState 적용.
## Player Folder
- Bullet.cs : 탄환 관련. 탄환 생성 / 업데이트 / 제거 / 이동 / 충돌 검사 등을 관리.
- BulletPool.cs : 생성 및 제거가 잦은 탄환을 큐를 이용해 오브젝트 풀링처럼 구현.
- Player.cs : 플레이어 상태 정의 및 업데이트, 스킬, 스프라이트, 이동로직, 탄환과 연결 등 관리
- Skill.cs : 플레이어가 사용할 Skill들을 정의. Skill 추상클래스를 통해 Q,W,E,R Skill 들을 각각 상속받아 구현.
## Redner Folder
- FieldRender.cs : 좌측 플레이 공간에 대한 정의 및 그리기 담당
- GameOverRender.cs : 게임 오버 화면 그리기 담당. 랭킹 서버 관련 로직 전부 포함.
- IntroRender.cs : 게임 시작화면 그리기 담당.
- LevelUpRender.cs : 레벨업시 출력되는 화면 그리기 담당.
- PauseRender.cs : 일시정지 화면 그리기 담당.
- RenderBuffer.cs : 콘솔 출력을 위한 2중 버퍼를 구현. 이전 프레임과 비교해 변경된 셀만 갱신하여 렌더링 최적화를 수행.
- Renderer.cs : 프레임 단위 렌더링 흐름을 관리하는 진입점. 콘솔 초기화, 프레임 시작과 끝 호출을 통해 렌더 버퍼를 제어.
- RenderSystem.cs : 텍스트 및 패턴을 그리기 위한 실질적인 렌더링 수행 메서드 구현.
- UIRender.cs : 플레이 화면의 우측 UI 부분 그리기 담당.
## Sound Folder
- BeepSound.cs : Console.Beep을 사용하기 편하도록 음계별로 정의한 소스파일
- Bgm.cs : 인트로에 사용된 간단한 Beep 루프
## Utils Folder
- AlphabetM.cs , AlphabetS.cs : 알파벳 및 숫자, 일부 특수문자 패턴 구현.
- ConsoleFontManager.cs : 콘솔 폰트와 글자 크기를 설정하는 클래스. Karnel32.dll 의 API를 호출해 폰트 종류와 픽셀 크기를 변경할 수 있도록 함.
- ConsoleWindowManager.cs : 콘솔 창 크기와 스타일 제어. Win32API를 통해 리사이즈 & 최대화 버튼 비활성화 및 스크롤바 제거 수행
## Root Directory
- FirebasePublicApi.cs : 파이어베이스 api.
- Program.cs : 메인. 업데이트 담당 및 전역 함수, 전역 변수 위주.
---

2025-10-07 : Last Updated


