 
<template>
    <n-card title="URI 在线编码解码" class="h-full shadow-sm rounded-16px">
        <n-list :bordered="false">
            <n-list-item>
                <n-input v-model:value="data.text" type="textarea" placeholder="请输入要进行 URI 编码或解码的字符" />
            </n-list-item>
            <n-list-item>
                <n-space>
                    <n-button type="success" @click="data.result = encodeURI(data.text)">
                        encodeURI
                    </n-button>
                    <n-button type="info" @click="data.result = decodeURI(data.text)">
                        decodeURI
                    </n-button>
                    <n-button type="info" @click="data.result = encodeURIComponent(data.text)">
                        encodeURIComponent
                    </n-button>
                    <n-button type="info" @click="data.result = decodeURIComponent(data.text)">
                        decodeURIComponent
                    </n-button>
                    <n-button @click="exchange">
                        交换
                    </n-button>
                </n-space>
            </n-list-item>
            <n-list-item>
                <n-input-group>
                    <n-input ref="md5Digit16" v-model:value="data.result" type="textarea" placeholder="转换结果" />
                </n-input-group>
            </n-list-item>
            <n-list-item>
                <n-button type="primary" ghost @click="copy(data.result)">
                    复制结果
                </n-button>
            </n-list-item>
        </n-list>
    </n-card>
</template>



<script lang="ts" setup>
const data = reactive({
    text: '',
    result: ''
})


const exchange = () => {
    const temp = data.text;
    data.text = data.result;
    data.result = temp;
}

const message = useMessage()
const copy = (text: string) => {
    navigator.clipboard.writeText(text);
    message.success("复制成功")
}


</script>