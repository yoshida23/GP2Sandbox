# ショットの仕様
ショットはインターフェースとScriptableObjectを使って、アイテムなどからプレイヤーに渡せるようにする。

## 保持
- シーン
  - オブジェクトプールをまとめておくオブジェクト ObjectPoolInitiator
    - シーン開始時にオブジェクトプールのオブジェクトを生成するためにInit()を呼び出す
- キャラクター
  - キャラクターには弾の種類の分、スロットを持たせる
    - 弾を撃つ処理と、使用するオブジェクトプールを設定したScriptableObjectをスロットにアタッチ
    - ScriptableObjectのstaticで、弾を撃てるかどうかを判定するIShotConstraintのインスタンスを生成して、ScriptableObjectに対応したスロットに保存
    - 弾を撃つ時は、IShotConstraint.Shot()を呼び出す
- ショットアイテム
  - 該当する弾を撃つScriptableObjectを持たせて、Playerなどの攻撃スロットに設定

## クラスやインターフェースの名前
- ShotAssetBase
  - 弾の撃ち方とオブジェクトプールを保持するScriptableObjectの抽象クラス
  - このクラスを継承して、弾の撃ち方と、射撃制約を管理するIShooterの派生クラスのインスタンスの生成を実装
  - 特定のIShooter実装クラスに依存
- IShooter
  - キャラクターにアタッチする制限を処理するクラスに実装するインターフェース
  - Shot(Transform)を宣言
  - Shot()は、射撃の可否を確認して、可能なら生成時に受け取っているShotAssetBase.Shot(this)を呼び出し
  - ShotAssetBaseのShotに必要なパラメーターを返すGetShotParam()を実装する
- AM1ObjectPool
  - オブジェクトを管理するScriptableObject
- ObjectPoolInitiator
  - オブジェクトプールの初期化を行って、生成したオブジェクトを保持しておくオブジェクトにアタッチするコンポーネントスクリプト
- ShooterSlot
  - ShotAssetBaseを渡してnewすると`ShooterInstance`に管理インスタンスを生成する
  - 弾を撃つキャラクターはこのクラスのインスタンスを持つようにして、Awake()やShotAssetBaseを受け取った時に`new ShooterSlot(shotasset)`を呼び出す
  - 弾を撃つ時はこのインスタンスの`.ShooterInstance.Shot(transform);`でショットを試みる。一発以上撃てたらtrueが返る
 
