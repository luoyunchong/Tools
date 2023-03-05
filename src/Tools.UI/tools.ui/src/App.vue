<script setup lang="ts">
</script>

<template>
    <n-loading-bar-provider>
        <n-dialog-provider class="h-full">
            <DialogContent />
            <n-notification-provider>
                <n-message-provider>
                    <MessageContent />
                    <RouterView />
                </n-message-provider>
            </n-notification-provider>
        </n-dialog-provider>
    </n-loading-bar-provider>
</template>

<style scoped></style>
