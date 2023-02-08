 
<template>
    <n-card title="MD5在线加密">
      <n-list bordered="false">
        <n-list-item>
          <n-input v-model:value="value" type="textarea" @keyup="convert" placeholder="请输入要转换的字符串" />
        </n-list-item>
        <n-list-item>
          <n-button type="primary" ghost @click="convert">
            MD5
          </n-button>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input ref="md5Digit16" v-model:value="data.md5.md5Digit16" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.md5Digit16)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input ref="md5Digit16Lower" v-model:value="data.md5.md5Digit16Lower" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.md5Digit16Lower)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input ref="md5Digit32" v-model:value="data.md5.md5Digit32" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.md5Digit32)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input ref="md5Digit32Lower" v-model:value="data.md5.md5Digit32Lower" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.md5Digit32Lower)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input ref="base64Md5Digit16" v-model:value="data.md5.base64Md5Digit16" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.base64Md5Digit16)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
        <n-list-item>
          <n-input-group>
            <n-input v-model:value="data.md5.base64Md5Digit32" type="text" placeholder="转换结果" />
            <n-button type="primary" ghost @click="copy(data.md5.base64Md5Digit32)">
              复制
            </n-button>
          </n-input-group>
        </n-list-item>
      </n-list>
    </n-card>
</template>

 
<script lang="ts" setup>
import API from '@/api';

const value = ref('');

let data = reactive({
  md5: {
    md5Digit32: '',
    md5Digit16: '',
    md5Digit32Lower: '',
    md5Digit16Lower: '',
    base64Md5Digit32: '',
    base64Md5Digit16: ''
  }
})
const convert = async () => {
  let result: Md5Output = await API.allmd5(value.value);
  console.log(result);
  data.md5 = result;
};

const message = useMessage()
const copy = (text: string) => {
  navigator.clipboard.writeText(text);
  message.success("复制成功")
}


</script>

<style lang="scss">
</style>
