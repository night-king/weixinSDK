/*
* 注意：
* 1. 所有的JS接口只能在公众号绑定的域名下调用，公众号开发者需要先登录微信公众平台进入“公众号设置”的“功能设置”里填写“JS接口安全域名”。
* 2. 如果发现在 Android 不能分享自定义内容，请到官网下载最新的包覆盖安装，Android 自定义分享接口需升级至 6.0.2.58 版本及以上。
* 3. 完整 JS-SDK 文档地址：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
*
* 如有问题请通过以下渠道反馈：
* 邮箱地址：weixin-open@qq.com
* 邮件主题：【微信JS-SDK反馈】具体问题
* 邮件内容说明：用简明的语言描述问题所在，并交代清楚遇到该问题的场景，可附上截屏图片，微信团队会尽快处理你的反馈。
*/
wx.ready(function () {
    // 1 判断当前版本是否支持指定 JS 接口，支持批量判断
    document.querySelector('#checkJsApi').onclick = function () {
        wx.checkJsApi({
            jsApiList: [
        'getNetworkType',
        'previewImage'
      ],
            success: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };

    // 2. 分享接口
    // 2.1 监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
    document.querySelector('#onMenuShareAppMessage').onclick = function () {
        wx.onMenuShareAppMessage({
            title: '互联网之子',
            desc: '在长大的过程中，我才慢慢发现，我身边的所有事，别人跟我说的所有事，那些所谓本来如此，注定如此的事，它们其实没有非得如此，事情是可以改变的。更重要的是，有些事既然错了，那就该做出改变。',
            link: 'http://movie.douban.com/subject/25785114/',
            imgUrl: 'http://demo.open.weixin.qq.com/jssdk/images/p2166127561.jpg',
            trigger: function (res) {
                alert('用户点击发送给朋友');
            },
            success: function (res) {
                alert('已分享');
            },
            cancel: function (res) {
                alert('已取消');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
        alert('已注册获取“发送给朋友”状态事件');
    };

    // 2.2 监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
    document.querySelector('#onMenuShareTimeline').onclick = function () {
        wx.onMenuShareTimeline({
            title: '互联网之子',
            link: 'http://movie.douban.com/subject/25785114/',
            imgUrl: 'http://demo.open.weixin.qq.com/jssdk/images/p2166127561.jpg',
            trigger: function (res) {
                alert('用户点击分享到朋友圈');
            },
            success: function (res) {
                alert('已分享');
            },
            cancel: function (res) {
                alert('已取消');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
        alert('已注册获取“分享到朋友圈”状态事件');
    };

    // 2.3 监听“分享到QQ”按钮点击、自定义分享内容及分享结果接口
    document.querySelector('#onMenuShareQQ').onclick = function () {
        wx.onMenuShareQQ({
            title: '互联网之子',
            desc: '在长大的过程中，我才慢慢发现，我身边的所有事，别人跟我说的所有事，那些所谓本来如此，注定如此的事，它们其实没有非得如此，事情是可以改变的。更重要的是，有些事既然错了，那就该做出改变。',
            link: 'http://movie.douban.com/subject/25785114/',
            imgUrl: 'http://img3.douban.com/view/movie_poster_cover/spst/public/p2166127561.jpg',
            trigger: function (res) {
                alert('用户点击分享到QQ');
            },
            complete: function (res) {
                alert(JSON.stringify(res));
            },
            success: function (res) {
                alert('已分享');
            },
            cancel: function (res) {
                alert('已取消');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
        alert('已注册获取“分享到 QQ”状态事件');
    };

    // 2.4 监听“分享到微博”按钮点击、自定义分享内容及分享结果接口
    document.querySelector('#onMenuShareWeibo').onclick = function () {
        wx.onMenuShareWeibo({
            title: '互联网之子',
            desc: '在长大的过程中，我才慢慢发现，我身边的所有事，别人跟我说的所有事，那些所谓本来如此，注定如此的事，它们其实没有非得如此，事情是可以改变的。更重要的是，有些事既然错了，那就该做出改变。',
            link: 'http://movie.douban.com/subject/25785114/',
            imgUrl: 'http://img3.douban.com/view/movie_poster_cover/spst/public/p2166127561.jpg',
            trigger: function (res) {
                alert('用户点击分享到微博');
            },
            complete: function (res) {
                alert(JSON.stringify(res));
            },
            success: function (res) {
                alert('已分享');
            },
            cancel: function (res) {
                alert('已取消');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
        alert('已注册获取“分享到微博”状态事件');
    };


    // 3 智能接口
    var voice = {
        localId: '',
        serverId: ''
    };
    // 3.1 识别音频并返回识别结果
    document.querySelector('#translateVoice').onclick = function () {
        if (voice.localId == '') {
            alert('请先使用 startRecord 接口录制一段声音');
            return;
        }
        wx.translateVoice({
            localId: voice.localId,
            complete: function (res) {
                if (res.hasOwnProperty('translateResult')) {
                    alert('识别结果：' + res.translateResult);
                } else {
                    alert('无法识别');
                }
            }
        });
    };

    // 4 音频接口
    // 4.2 开始录音
    document.querySelector('#startRecord').onclick = function () {
        wx.startRecord({
            cancel: function () {
                alert('用户拒绝授权录音');
            }
        });
    };

    // 4.3 停止录音
    document.querySelector('#stopRecord').onclick = function () {
        wx.stopRecord({
            success: function (res) {
                voice.localId = res.localId;
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };

    // 4.4 监听录音自动停止
    wx.onVoiceRecordEnd({
        complete: function (res) {
            voice.localId = res.localId;
            alert('录音时间已超过一分钟');
        }
    });

    // 4.5 播放音频
    document.querySelector('#playVoice').onclick = function () {
        if (voice.localId == '') {
            alert('请先使用 startRecord 接口录制一段声音');
            return;
        }
        wx.playVoice({
            localId: voice.localId
        });
    };

    // 4.6 暂停播放音频
    document.querySelector('#pauseVoice').onclick = function () {
        wx.pauseVoice({
            localId: voice.localId
        });
    };

    // 4.7 停止播放音频
    document.querySelector('#stopVoice').onclick = function () {
        wx.stopVoice({
            localId: voice.localId
        });
    };

    // 4.8 监听录音播放停止
    wx.onVoicePlayEnd({
        complete: function (res) {
            alert('录音（' + res.localId + '）播放结束');
        }
    });

    // 4.8 上传语音
    document.querySelector('#uploadVoice').onclick = function () {
        if (voice.localId == '') {
            alert('请先使用 startRecord 接口录制一段声音');
            return;
        }
        wx.uploadVoice({
            localId: voice.localId,
            success: function (res) {
                alert('上传语音成功，serverId 为' + res.serverId);
                voice.serverId = res.serverId;
            }
        });
    };

    // 4.9 下载语音
    document.querySelector('#downloadVoice').onclick = function () {
        if (voice.serverId == '') {
            alert('请先使用 uploadVoice 上传声音');
            return;
        }
        wx.downloadVoice({
            serverId: voice.serverId,
            success: function (res) {
                alert('下载语音成功，localId 为' + res.localId);
                voice.localId = res.localId;
            }
        });
    };

    // 5 图片接口
    // 5.1 拍照、本地选图
    var images = {
        localId: [],
        serverId: []
    };
    document.querySelector('#chooseImage').onclick = function () {
        wx.chooseImage({
            success: function (res) {
                images.localId = res.localIds;
                alert('已选择 ' + res.localIds.length + ' 张图片');
            }
        });
    };

    // 5.2 图片预览
    document.querySelector('#previewImage').onclick = function () {
        wx.previewImage({
            current: 'http://img5.douban.com/view/photo/photo/public/p1353993776.jpg',
            urls: [
        'http://img3.douban.com/view/photo/photo/public/p2152117150.jpg',
        'http://img5.douban.com/view/photo/photo/public/p1353993776.jpg',
        'http://img3.douban.com/view/photo/photo/public/p2152134700.jpg'
      ]
        });
    };

    // 5.3 上传图片
    document.querySelector('#uploadImage').onclick = function () {
        if (images.localId.length == 0) {
            alert('请先使用 chooseImage 接口选择图片');
            return;
        }
        var i = 0, length = images.localId.length;
        images.serverId = [];
        function upload() {
            wx.uploadImage({
                localId: images.localId[i],
                success: function (res) {
                    i++;
                    alert('已上传：' + i + '/' + length);
                    images.serverId.push(res.serverId);
                    if (i < length) {
                        upload();
                    }
                },
                fail: function (res) {
                    alert(JSON.stringify(res));
                }
            });
        }
        upload();
    };

    // 5.4 下载图片
    document.querySelector('#downloadImage').onclick = function () {
        if (images.serverId.length === 0) {
            alert('请先使用 uploadImage 上传图片');
            return;
        }
        var i = 0, length = images.serverId.length;
        images.localId = [];
        function download() {
            wx.downloadImage({
                serverId: images.serverId[i],
                success: function (res) {
                    i++;
                    alert('已下载：' + i + '/' + length);
                    images.localId.push(res.localId);
                    if (i < length) {
                        download();
                    }
                }
            });
        }
        download();
    };

    // 6 设备信息接口
    // 6.1 获取当前网络状态
    document.querySelector('#getNetworkType').onclick = function () {
        wx.getNetworkType({
            success: function (res) {
                alert(res.networkType);
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };

    // 7 地理位置接口
    // 7.1 查看地理位置
    document.querySelector('#openLocation').onclick = function () {
        wx.openLocation({
            latitude: 23.099994,
            longitude: 113.324520,
            name: 'TIT 创意园',
            address: '广州市海珠区新港中路 397 号',
            scale: 14,
            infoUrl: 'http://weixin.qq.com'
        });
    };

    // 7.2 获取当前地理位置
    document.querySelector('#getLocation').onclick = function () {
        wx.getLocation({
            success: function (res) {
                alert(JSON.stringify(res));
            },
            cancel: function (res) {
                alert('用户拒绝授权获取地理位置');
            }
        });
    };

    // 8 界面操作接口
    // 8.1 隐藏右上角菜单
    document.querySelector('#hideOptionMenu').onclick = function () {
        wx.hideOptionMenu();
    };

    // 8.2 显示右上角菜单
    document.querySelector('#showOptionMenu').onclick = function () {
        wx.showOptionMenu();
    };

    // 8.3 批量隐藏菜单项
    document.querySelector('#hideMenuItems').onclick = function () {
        wx.hideMenuItems({
            menuList: [
        'menuItem:readMode', // 阅读模式
        'menuItem:share:timeline', // 分享到朋友圈
        'menuItem:copyUrl' // 复制链接
      ],
            success: function (res) {
                alert('已隐藏“阅读模式”，“分享到朋友圈”，“复制链接”等按钮');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };

    // 8.4 批量显示菜单项
    document.querySelector('#showMenuItems').onclick = function () {
        wx.showMenuItems({
            menuList: [
        'menuItem:readMode', // 阅读模式
        'menuItem:share:timeline', // 分享到朋友圈
        'menuItem:copyUrl' // 复制链接
      ],
            success: function (res) {
                alert('已显示“阅读模式”，“分享到朋友圈”，“复制链接”等按钮');
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };

    // 8.5 隐藏所有非基本菜单项
    document.querySelector('#hideAllNonBaseMenuItem').onclick = function () {
        wx.hideAllNonBaseMenuItem({
            success: function () {
                alert('已隐藏所有非基本菜单项');
            }
        });
    };

    // 8.6 显示所有被隐藏的非基本菜单项
    document.querySelector('#showAllNonBaseMenuItem').onclick = function () {
        wx.showAllNonBaseMenuItem({
            success: function () {
                alert('已显示所有非基本菜单项');
            }
        });
    };

    // 8.7 关闭当前窗口
    document.querySelector('#closeWindow').onclick = function () {
        wx.closeWindow();
    };

    // 9 微信原生接口
    // 9.1.1 扫描二维码并返回结果
    document.querySelector('#scanQRCode0').onclick = function () {
        wx.scanQRCode();
    };
    // 9.1.2 扫描二维码并返回结果
    document.querySelector('#scanQRCode1').onclick = function () {
        wx.scanQRCode({
            needResult: 1,
            desc: 'scanQRCode desc',
            success: function (res) {
                alert(JSON.stringify(res));
            }
        });
    };
    // 11.3  跳转微信商品页
    document.querySelector('#openProductSpecificView').onclick = function () {
        wx.openProductSpecificView({
            productId: 'pDF3iY_m2M7EQ5EKKKWd95kAxfNw'
        });
    };

    // 12 微信卡券接口
    // 12.1 添加卡券
    document.querySelector('#addCard').onclick = function () {
        wx.addCard({
            cardList: [
        {
            cardId: 'pDF3iY9tv9zCGCj4jTXFOo1DxHdo',
            cardExt: '{"code": "", "openid": "", "timestamp": "1418301401", "signature":"64e6a7cc85c6e84b726f2d1cbef1b36e9b0f9750"}'
        },
        {
            cardId: 'pDF3iY9tv9zCGCj4jTXFOo1DxHdo',
            cardExt: '{"code": "", "openid": "", "timestamp": "1418301401", "signature":"64e6a7cc85c6e84b726f2d1cbef1b36e9b0f9750"}'
        }
      ],
            success: function (res) {
                alert('已添加卡券：' + JSON.stringify(res.cardList));
            }
        });
    };

    // 12.2 选择卡券
    document.querySelector('#chooseCard').onclick = function () {
        wx.chooseCard({
            cardSign: '97e9c5e58aab3bdf6fd6150e599d7e5806e5cb91',
            timestamp: 1417504553,
            nonceStr: 'k0hGdSXKZEj3Min5',
            success: function (res) {
                alert('已选择卡券：' + JSON.stringify(res.cardList));
            }
        });
    };

    // 12.3 查看卡券
    document.querySelector('#openCard').onclick = function () {
        alert('您没有该公众号的卡券无法打开卡券。');
        wx.openCard({
            cardList: [
      ]
        });
    };

    var shareData = {
        title: '微信JS-SDK Demo',
        desc: '微信JS-SDK,帮助第三方为用户提供更优质的移动web服务',
        link: 'http://demo.open.weixin.qq.com/jssdk/',
        imgUrl: 'http://mmbiz.qpic.cn/mmbiz/icTdbqWNOwNRt8Qia4lv7k3M9J1SKqKCImxJCt7j9rHYicKDI45jRPBxdzdyREWnk0ia0N5TMnMfth7SdxtzMvVgXg/0'
    };
    wx.onMenuShareAppMessage(shareData);
    wx.onMenuShareTimeline(shareData);
});

wx.error(function (res) {
    alert(res.errMsg);
});
