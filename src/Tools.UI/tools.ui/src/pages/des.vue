 
<template>
    <div class="h-full">
        <n-card title="DES分组密码（块密码）加密技术" class="h-full shadow-sm rounded-16px">
            <n-list :bordered="false">
                <n-list-item>
                    <n-input v-model:value="data.des.sourceString" type="textarea" placeholder="请输入要转换的字符串" />
                </n-list-item>
                <n-list-item>
                    <n-input v-model:value="data.des.password" type="input" placeholder="密钥" />
                </n-list-item>
                <n-list-item>
                    <n-space>
                        <n-button type="primary" ghost @click="encrypt()">
                            DES加密
                        </n-button>
                        <n-button type="primary" @click="decrypt()">
                            DES解密
                        </n-button>
                    </n-space>
                </n-list-item>
                <n-list-item>
                    <n-input v-model:value="data.result" type="textarea" placeholder="转换结果" />
                </n-list-item>
                <n-list-item>
                    <n-button type="primary" ghost @click="copy(data.result)">
                        复制结果
                    </n-button>
                </n-list-item>
            </n-list>

            <n-card>
                <p>　DES是一种对称密钥的块加密算法。谓之 “对称密钥”，是因为加密、解密用的密钥是一样的（这不同于 RSA 等非对称密钥体系）。谓之
                    “块加密”，是因为这种算法把明文划分为很多个等长的块(block)，对每个块进行加密，最后以某种手段拼在一起。“块加密”亦称“分组加密”。</p>
            </n-card>
        </n-card>
    </div>
</template>

 
<script lang="ts" setup>
import API from '@/api';


const value = ref('');

let data = reactive({
    des: {
        sourceString: '',
        password: ''
    },
    result: ''
})


const encrypt = async () => {
    let result: string = await API.encrypt(data.des);
    data.result = result;
};

const decrypt = async () => {
    let result: string = await API.decrypt(data.des);
    data.result = result;
};

const message = useMessage()
const copy = (text: string) => {
    navigator.clipboard.writeText(text);
    message.success("复制成功")
}


</script>

<style lang="scss"></style>
