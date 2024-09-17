<template>
  <div class="bg-#235">
    <div class="player"></div>
    <div class="flex p-2 form-control">
      <n-input v-model:value="option.url" style="width: 80%" />
      <n-button type="info" class="m-l-2" @click="playVideo">播放</n-button>
    </div>
  </div>
</template>

<script>
import Artplayer from "artplayer";
import artplayerPluginHlsQuality from "artplayer-plugin-hls-quality";
import artplayerPluginControl from "artplayer-plugin-control";
import Hls from "hls.js";

function playM3u8(video, url, art) {
  if (Hls.isSupported()) {
    if (art.hls) art.hls.destroy();
    const hls = new Hls();
    hls.loadSource(url);
    hls.attachMedia(video);
    art.hls = hls;
    art.on("destroy", () => hls.destroy());
  } else if (video.canPlayType("application/vnd.apple.mpegurl")) {
    video.src = url;
  } else {
    art.notice.show = "Unsupported playback format: m3u8";
  }
}

let demo_video_url = "https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8";
export default {
  data() {
    return {
      option: {
        container: ".player",
        type: "m3u8",
        url: demo_video_url,
        title: "m3u8播放器",
        loop: true, // 区间循序播放
        flip: true, // 画面翻转
        playbackRate: true, // 播放速度
        aspectRatio: true, // 画面比例
        screenshot: true, // 截屏
        setting: true, // 设置
        pip: true, // 画中画
        fullscreenWeb: true, // 网页全屏
        fullscreen: true, // 全屏
        subtitleOffset: true, // 字幕偏移
        miniProgressBar: true, // 迷你进度条
        airplay: true,
        theme: "#23ade5",
        thumbnails: {},
        subtitle: {},
        highlight: [
          {
            time: 15,
            text: "欢迎使用m3u8播放器",
          },
        ],
        icons: {
          loading:
            '<img src="images/loading.gif" width="100px" title="视频加载中..." />',
        },
        settings: [
          {
            html: "控件栏浮动",
            icon: '<img width="22" heigth="22" src="images/state.svg">',
            tooltip: "开启",
            switch: true,
            onSwitch: async (item) => {
              item.tooltip = item.switch ? "关闭" : "开启";
              art.plugins.artplayerPluginControl.enable = !item.switch;
              await Artplayer.utils.sleep(300);
              art.setting.updateStyle();
              return !item.switch;
            },
          },
        ],
        customType: {
          m3u8: playM3u8,
        },
        plugins: [
          artplayerPluginControl({
            maxWidth: 600,
          }),
          artplayerPluginHlsQuality({
            control: true, // 显示在控制栏
            setting: false, // 显示在设置
            title: "Quality", // I18n
            auto: "Auto",
          }),
        ],
      },
      art: null,
    };
  },
  components: {},
  mounted() {
    var video_url = this.$route.query.video_url;
    if (video_url) {
      this.option.url = video_url;
    }
    this.playVideo();
  },
  beforeDestroy() {
    if (this.art && this.art.destroy) {
      this.art.destroy(false);
    }
  },
  methods: {
    playVideo() {
      if (!this.option.url) {
        window.$message.warning("请输入视频地址");
        return;
      }

      if (this.art?.id) {
        this.art.destroy();
      }
      try {
        this.art = new Artplayer(this.option);
        this.art.on("ready", () => {
          setTimeout(() => {
            // window.$message.success("开始播放");
            this.art.play();
          }, 100);
        });
      } catch (e) {
        console.error("发生异常:", e);
      }
    },
  },
};
</script>

<style scoped>
.player {
  width: 100%;
  color: #fff;
  text-align: center;
  height: calc(100vh - 50px);
}
.player :deep(.artplayer-plugin-control .art-bottom) {
  width: 500px;
  margin-left: -250px;
}
</style>
