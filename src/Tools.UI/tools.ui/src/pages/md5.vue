 
<template>
    <div class="h-full">
        <n-card title="MD5在线加密" class="h-full shadow-sm rounded-16px">
            <n-form>
                <n-form-item label="字符串">
                    <n-input v-model:value="value" type="textarea" @keyup="convert" placeholder="请输入要转换的字符串" />
                </n-form-item>
                <div style="display: flex; justify-content: flex-start;margin-bottom: 20px;">
                    <n-button type="primary" ghost @click="convert">
                        MD5
                    </n-button>
                </div>
                <n-form-item label="16位">
                    <n-input-group>
                        <n-input ref="md5Digit16" v-model:value="data.md5.md5Digit16" type="text" placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.md5Digit16)">
                            复制
                        </n-button>
                    </n-input-group>
                </n-form-item>
                <n-form-item label="16位小写">
                    <n-input-group>
                        <n-input ref="md5Digit16Lower" v-model:value="data.md5.md5Digit16Lower" type="text"
                            placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.md5Digit16Lower)">
                            复制
                        </n-button>
                    </n-input-group>
                </n-form-item>
                <n-form-item label="32位">
                    <n-input-group>
                        <n-input ref="md5Digit32" v-model:value="data.md5.md5Digit32" type="text" placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.md5Digit32)">
                            复制
                        </n-button>
                    </n-input-group>
                </n-form-item>
                <n-form-item label="32位小写">
                    <n-input-group>
                        <n-input ref="md5Digit32Lower" v-model:value="data.md5.md5Digit32Lower" type="text"
                            placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.md5Digit32Lower)">
                            复制
                        </n-button>
                    </n-input-group>
                </n-form-item>
                <n-form-item label="BASE64 16位">
                    <n-input-group>
                        <n-input ref="base64Md5Digit16" v-model:value="data.md5.base64Md5Digit16" type="text"
                            placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.base64Md5Digit16)">
                            复制
                        </n-button>
                    </n-input-group></n-form-item>
                <n-form-item label="BASE64 32">
                    <n-input-group>
                        <n-input v-model:value="data.md5.base64Md5Digit32" type="text" placeholder="转换结果" />
                        <n-button type="primary" ghost @click="copy(data.md5.base64Md5Digit32)">
                            复制
                        </n-button>
                    </n-input-group>
                </n-form-item>
            </n-form>
        </n-card>
    </div>
</template>

 
<script lang="ts" setup>
import API from "@/api";

const value = ref("");

let data = reactive({
    md5: {
        md5Digit32: "",
        md5Digit16: "",
        md5Digit32Lower: "",
        md5Digit16Lower: "",
        base64Md5Digit32: "",
        base64Md5Digit16: "",
    },
});
const convert = async () => {
    let result: Md5Output = await API.allmd5(value.value);
    console.log(result);
    data.md5 = result;
};

const message = useMessage();
const copy = (text: string) => {
    navigator.clipboard.writeText(text);
    message.success("复制成功");
};
</script>

<style lang="scss"></style>
