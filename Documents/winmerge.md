# WinMergeを使ったマージ

Gitでの自動マージは、マージする項目の様子が見えない不安があります。マージツールであるWinMergeを使うことで、変更内容を確認しながらマージすることができます。必要に応じて利用するとよいでしょう。

## プロジェクトの準備



WinMergeで確認しながら手動でマージするには、比較元と比較先の2つのプロジェクトが必要なのでプロジェクトをコピーします。

1. GitHub DesktopのRepositoryメニューから Show in Explorer でプロジェクトフォルダーを開く
1. 一つ親のフォルダーに移動

![親フォルダーへ](./images/winmerge/img00.png)

3. 新しいフォルダーを作成して`merge`などの名前にしておく
1. プロジェクトフォルダーをコピーして、merge フォルダー内に貼り付け
1. GitHub Desktop に切り替え
1. Fileメニューから Add local repository を選択
1. Choose をクリックして、`merge`フォルダーにコピーしたプロジェクトフォルダーを選択して、Add repositoryで開く
