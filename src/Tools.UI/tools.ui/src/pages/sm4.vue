<template>
    <n-card title="国密SM4">
        <n-tabs type="line" animated>
            <n-tab-pane name="oasis" tab="加密">
                <n-list :bordered="false">
                    <n-list-item>
                        <n-input v-model:value="data.text" type="textarea" placeholder="请输入要进行加密/解密的内容" />
                    </n-list-item>
                    <n-list-item>
                        <n-input v-model:value="data.keys" type="text" placeholder="Keys" />
                    </n-list-item>
                    <n-list-item>
                        <n-space>
                            <n-button type="success" @click="encryptECB">
                                加密
                            </n-button>
                            <n-button type="info" @click="decryptDataECB(0)">
                                解密Base64
                            </n-button>

                            <n-button type="info" @click="decryptDataECB(1)">
                                解密Hex
                            </n-button>
                        </n-space>
                    </n-list-item>
                    <n-list-item>
                        <n-input-group>
                            <n-input v-model:value="data.sm4base64" type="textarea" placeholder="转换结果" />
                        </n-input-group>
                    </n-list-item>
                    <n-list-item>
                        <n-button type="primary" ghost @click="copy(data.sm4base64)">
                            复制结果
                        </n-button>
                    </n-list-item>
                    <n-list-item>
                        <n-input-group>
                            <n-input v-model:value="data.sm4hex" type="textarea" placeholder="转换结果" />
                        </n-input-group>
                    </n-list-item>
                    <n-list-item>
                        <n-button type="primary" ghost @click="copy(data.sm4hex)">
                            复制结果
                        </n-button>
                    </n-list-item>
                </n-list>
            </n-tab-pane>
            <n-tab-pane name="the beatles" tab="解密">
                <n-list :bordered="false">
                    <n-list-item>
                        <n-input v-model:value="decryptData.text" type="textarea" placeholder="请输入要进行解密的内容" />
                    </n-list-item>
                    <n-list-item>
                        <n-input v-model:value="decryptData.keys" type="text" placeholder="Keys" />
                    </n-list-item>
                    <n-list-item>
                        <n-space>
                            <n-button type="info" @click="decryptDataECB(0)">
                                解密Base64
                            </n-button>
                            <n-button type="info" @click="decryptDataECB(1)">
                                解密Hex
                            </n-button>
                        </n-space>
                    </n-list-item>
                    <n-list-item>
                        <n-input-group>
                            <n-input v-model:value="decryptData.result" type="textarea" placeholder="转换结果" />
                        </n-input-group>
                    </n-list-item>
                    <n-list-item>
                        <n-button type="primary" ghost @click="copy(decryptData.result)">
                            复制结果
                        </n-button>
                    </n-list-item>
                </n-list>
            </n-tab-pane>
        </n-tabs>
    </n-card>

</template>

<script lang="ts" setup>
    interface SM4ECBOutput {
        text: string;
        keys: string;
        sm4Base64: string;
        sm4Hex: string;
    }
    import API from '@/api';

    const data = reactive<SM4ECBOutput>({
        text: '',
        keys: '',
        sm4base64: '',
        sm4hex: '',
    })

    const decryptData = reactive<SM4ECBOutput>({
        text: '',
        keys: '',
        result: ''
    })


    const encryptECB = () => {
        API.EcbPadding({ data: data.text, keys: data.keys, enOrDecrpyt: true }).then((res: SM4ECBOutput) => {
            data.sm4base64 = res.sm4Base64
            data.sm4hex = res.sm4Hex
        });
    }

    const decryptDataECB = (type: Number) => {
        API.EcbPadding({ data: decryptData.text, keys: decryptData.keys, enOrDecrpyt: false, type: type }).then((res: SM4ECBOutput) => {
            decryptData.result = res.text
        });
    }


    const message = useMessage()
    const copy = (text: string) => {
        navigator.clipboard.writeText(text);
        message.success("复制成功")
    }


</script>