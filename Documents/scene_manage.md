# シーン管理方法

シーン管理は`SceneChanger`スクリプトの`Change()`メソッドに、切り替えたいシーン切り替えファイル`SceneBase<>`の`Instance`を渡すことで実行します。

- SceneChanger
  - SystemシーンのSystemゲームオブジェクトにアタッチ。SceneChanger.Change(シーンのインスタンス)でシーン切り替え
  - シーン切り替え中は`IsChanging`がtrueになる
  - 各シーンに配置する??ManagerクラスがAwakeした時点でコールバックを受け取って、初期化のタイミングに呼び出す
- ??Scene
  - ISceneインターフェースを実装したシーン切り替えのためのクラス。非MonoBehaviour
  - コルーチンでシーンを切り替える処理のStartScene()と、シーンを終了する時のEndScene()を実装
  - `??Scene.Instance`でインスタンスを読み出せるので、それを`SceneChanger.Change(??Scene.Instance);`として呼び出せばシーン切り替えできる
- SimpleSingleton<T>
  - シングルトンを提供するジェネリッククラス
  - ??Sceneには必須
- IScene
  - ??Sceneに実装させるインターフェース
  - Change()とRelease()を定義
- ??Manager
  - 各シーンのゲームオブジェクトにアタッチしておくシーンオブジェクトを制御するクラス
  - Awakeで初期化用のコードをSceneChangerに報告して、他の処理が終わったら呼び出してもらう
