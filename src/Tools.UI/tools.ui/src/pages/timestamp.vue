 
<template>
    <n-card title="时间戳转换" class="h-full shadow-sm rounded-16px">
        <n-form :style="{
            maxWidth: '840px',
        }">
            <n-form-item label="当前时间戳（ms)">
                <n-space>
                    <n-input v-model:value="data.currentMillStamp" clearable />
                    <n-button type="success" @click="getcurrentMillStamp()">
                        获取
                    </n-button>
                </n-space>
            </n-form-item>
            <n-form-item label="当前时间戳(s)">
                <n-space>
                    <n-input v-model:value="data.currentStamp" clearable />
                    <n-button type="success" @click="getCurrentStamp()"> 获取 </n-button>
                </n-space>
            </n-form-item>
            <n-form-item label="时间戳">
                <n-space>
                    <n-input v-model:value="data.text" clearable />
                    <n-select v-model:value="data.convertType" :options="selectOptions" class="w-120px" />
                    <n-button type="success" @click="convertTimeStamp(data.text)">
                        转换
                    </n-button>
                    <n-input v-model:value="data.result" clearable :style="{ width: '300px' }" :autosize="false" />
                    <n-button type="primary" ghost @click="copy(data.result)">
                        复制结果
                    </n-button>
                </n-space>
            </n-form-item>
            <n-form-item label="时间">
                <n-space>
                    <n-input v-model:value="dataTime.text" type="datetime" clearable />
                    <n-select v-model:value="dataTime.convertType" :options="selectOptions" class="w-120px" />
                    <n-button type="success" @click="convertTimeToStamp(dataTime.text)">
                        转换
                    </n-button>
                    <n-input v-model:value="dataTime.result" clearable :style="{ width: '300px' }" :autosize="false" />
                    <n-button type="primary" ghost @click="copy(dataTime.result)">
                        复制结果
                    </n-button>
                </n-space>
            </n-form-item>
        </n-form>
    </n-card>
</template>

<script lang="ts" setup>
const data = reactive({
    currentMillStamp: 0,
    currentStamp: 0,
    text: "",
    convertType: "ms",
    result: "",
});

const dataTime = reactive({
    text: "",
    convertType: "ms",
    result: "",
});

const getcurrentMillStamp = () => {
    data.currentMillStamp = new Date().getTime();
};
const getCurrentStamp = () => {
    data.currentStamp = parseInt((new Date().getTime() / 1000).toString());
};

const selectOptions = reactive([
    {
        label: "秒(s)",
        value: "s",
    },
    {
        label: "毫秒(ms)",
        value: "ms",
    },
]);

function getDate(text: string, convertType: string) {
    let startDate: number;
    if (data.convertType == "s") {
        startDate = parseInt(text) * 1000;
    } else {
        startDate = parseInt(text);
    }
    let newDate = new Date(startDate).getTime() - 1000 * 60 * 60 * 24;
    let date = new Date(newDate);
    let y = date.getFullYear();
    let m = (date.getMonth() + 1 + "").padStart(2, "0");
    let d = (date.getDate() + 1 + "").padStart(2, "0");
    const hh = date.getHours().toString().padStart(2, "0"); // 小时
    const mm = date.getMinutes().toString().padStart(2, "0"); // 分钟
    const ss = date.getSeconds().toString().padStart(2, "0"); // 秒
    const millss = date.getMilliseconds().toString().padStart(2, "0"); // 秒
    if (convertType == "s") {
        return [y, m, d].join("-") + " " + [hh, mm, ss].join(":");
    } else {
        return [y, m, d].join("-") + " " + [hh, mm, ss, millss].join(":");
    }
}

const convertTimeStamp = (text: string) => {
    data.result = getDate(text, data.convertType);
};

const exchange = () => {
    const temp = data.text;
    data.text = data.result;
    data.result = temp;
};

const message = useMessage();
const copy = (text: string) => {
    navigator.clipboard.writeText(text);
    message.success("复制成功");
};

const convertTimeToStamp = (date: string) => {
    if (dataTime.convertType == "s") {
        dataTime.result = parseInt(
            (new Date(date).getTime() / 1000).toString()
        ).toString();
    } else {
        dataTime.result = new Date(date).getTime().toString();
    }
};

var setup = () => {
    getcurrentMillStamp();
    getCurrentStamp();
};

setup();
</script>
