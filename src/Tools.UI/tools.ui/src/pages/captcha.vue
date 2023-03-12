 
<template>
    <n-card title="Captcha图片验证码" class="h-full shadow-sm rounded-16px">
        <div class="m-b-10px">
            <n-list :bordered="false">
                <n-list-item>
                    <n-space align="center">
                        宽度：
                        <n-input-number v-model:value="data.width" />
                    </n-space>
                </n-list-item>
                <n-list-item>
                    <n-space>
                        <img :src="src" :width="data.width" />
                        <n-button type="primary" ghost @click="generCaptch()">获取验证码</n-button>
                    </n-space>
                </n-list-item>
            </n-list>
        </div>
    </n-card>
</template>

<script lang="ts" setup>
const BASEURL = import.meta.env.VITE_APP_URL;
const src = ref("");
const generCaptch = () => {
    src.value = BASEURL + "api/captcha/img?d=" + new Date().getTime();
};

const data = reactive({
    width: 300,
    height: 150,
});
generCaptch();
</script>
